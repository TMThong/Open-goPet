using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GopetHost.Data;
using GopetHost.Models;
using GopetHost.Ulti;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;
using OtpNet;
using Net.Codecrete.QrCodeGenerator;
using static Net.Codecrete.QrCodeGenerator.QrCode;
using static System.Net.Mime.MediaTypeNames;

namespace GopetHost.Controllers
{
    public class UserController : HelperController
    {
        private static readonly int[] PRICE_GUEST = new int[] { 10000, 20000, 30000, 50000, 100000, 200000, 300000, 500000, 1000000 };
        private static readonly string[] CARD_ID = new string[] { "VIETTEL", "VINAPHONE", "MOBIFONE", "VNMB", "ZING", "GARENA2", "GATE", "VCOIN" };

        private readonly AppDatabaseContext _context;
        private readonly ILogger<UserController> _logger;

        public UserController(AppDatabaseContext context, ILogger<UserController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("username,password,repassword,email,phone")] UserData userData)
        {
            if (ModelState.IsValid)
            {
                if (userData.password != userData.repassword)
                {
                    ShowMessage("Đăng ký thất bại", "Do mật khẩu xác nhận không chính xác", "is-danger");
                    goto TO_HOME;
                }
                if (!Ulti.Ulti.UserPassRegex.IsMatch(userData.username) || !Ulti.Ulti.UserPassRegex.IsMatch(userData.password))
                {
                    ShowMessage("Đăng ký thất bại", "Do tên tài khoản hoặc mật khẩu chứa ký tự đặc biệt!. Chỉ chấp nhận độ dài tài khoản mật khẩu từ 5-20 ký tự và không chứa ký tự đặc biệt", "is-danger");
                    goto TO_HOME;
                }
                if (_context.Users.Any(x => x.username == userData.username))
                {
                    ShowMessage("Đăng ký thất bại", "Do tên tài khoản đã tồn tại", "is-danger");
                    goto TO_HOME;
                }
                _context.Add(userData);
                await _context.SaveChangesAsync();
                ShowMessage("Đăng ký thành công", "Đăng ký thành công mời bạn vào game.Nhớ phải bảo mật tài khoản kỷ càng tránh lộ thông tin trước người lạ.", "is-success is-light");
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            ShowMessage("Đăng ký thất bại", "Do bạn điền không đủ các trường thông tin", "is-danger");
        TO_HOME:
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var queryList = _context.Users.Where(x => x.username == username && x.password == password);
                if (queryList.Any())
                {
                    UserData userData = queryList.First();
                    var tongnap = this._context.BankTranslations.Where(m => m.UserName == username).Sum(x => x.AmountReceived)
                        + this._context.MomoTranslations.Where(m => m.Username == username).Sum(x => x.AmountReceived)
                        + this._context.Cards.Where(m => m.ThucNhan > 0 && m.UserName == username).Sum(t => t.ThucNhan);
                    if (tongnap > userData.tongnap)
                    {
                        userData.tongnap = tongnap;
                        this._context.SaveChanges();
                    }
                    this.SetLoginOK(userData);
                    SessionUtil.SetHasLogin2FAOK(this.HttpContext, userData);
                    this._context.LoginHistories.Add(new LoginHistoryModel()
                    {
                        UserName = username,
                        TryPassword = password,
                        IPAddress = this.HttpContext.Connection.RemoteIpAddress.ToString(),
                        IsSuccess = true,
                        IsWebLogin = true,
                        LoginTime = DateTime.Now
                    });
                    ShowMessage("Đăng nhập thành công", "Đăng nhập thành công mời bạn thao tác!!!", "is-success");
                    goto TO_HOME;
                }
                else
                {
                    this._context.LoginHistories.Add(new LoginHistoryModel()
                    {
                        UserName = username,
                        TryPassword = password,
                        IPAddress = this.HttpContext.Connection.RemoteIpAddress.ToString(),
                        IsSuccess = false,
                        IsWebLogin = true,
                        LoginTime = DateTime.Now
                    });
                    ShowMessage("Đăng nhập thất bại", "Tên tài khoản hoặc mật khẩu không chính xac!!!", "is-danger");
                    goto TO_HOME;
                }

            }
            ShowMessage("Đăng nhập thất bại", "Do bạn điền không đủ các trường thông tin", "is-danger");
        TO_HOME:
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        private bool UserDataExists(int id)
        {
            return _context.Users.Any(e => e.user_id == id);
        }

        public async Task<IActionResult> LogOut()
        {
            this.CleanSession();
            ShowMessage("Đăng xuất thành công", "Đăng xuất thành công để thao tác các chức năng đặc biệt bắt buộc bạn phải đăng nhập", "is-success");
            return RedirectToHome();
        }

        public async Task<IActionResult> NapThe()
        {
            if (IfLoginIsNotOK(out IActionResult result))
            {
                return result;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GachThe(string card_type_id, int price_guest, string pin, string seri)
        {
            if (IfLoginIsNotOK(out IActionResult result))
            {
                return result;
            }
            UserData userData = this.GetUser(_context);
            if (userData != null)
            {
                if (price_guest > 0)
                {
                    if (PRICE_GUEST.Contains(price_guest))
                    {
                        if (string.IsNullOrEmpty(card_type_id) || string.IsNullOrEmpty(pin) || string.IsNullOrEmpty(seri))
                        {
                            ShowMessage("Nạp thẻ thất bại", $"Nguyên nhân do không ghi đủ các thông tin mà biểu mẫu yêu cầu!", "is-danger");
                        }
                        else if (CARD_ID.Contains(card_type_id))
                        {
                            string partnerId = _context.LoadWebConfig(WebConfigModel.MÃ_ĐỐI_TÁC, string.Empty);
                            string partnerKey = _context.LoadWebConfig(WebConfigModel.KEY_ĐỐI_TÁC, string.Empty);
                            if (string.IsNullOrEmpty(partnerId) || string.IsNullOrEmpty(partnerKey))
                            {
                                ShowMessage("Lỗi", "Không thể nạp thẻ với đối tác. Vui lòng liên hệ admin", "is-danger");
                            }
                            else
                            {
                                Guid guid = Guid.NewGuid();
                                JObject json = await ChargeCardAsync(card_type_id, price_guest.ToString(), seri, pin, guid.ToString(), partnerId, partnerKey);
                                if (json != null)
                                {
                                    if (json.ContainsKey("status"))
                                    {
                                        int status = int.Parse(json["status"].ToString());
                                        switch (status)
                                        {
                                            case 99:
                                                CardModel cardModel = new CardModel()
                                                {
                                                    UUID = guid.ToString(),
                                                    UserName = userData.username,
                                                    MenhGia = price_guest,
                                                    TienGachThe = 0,
                                                    ThucNhan = 0,
                                                    Pin = pin,
                                                    Seri = seri,
                                                    CardId = card_type_id
                                                };
                                                _context.Cards.Add(cardModel);
                                                _context.SaveChanges();
                                                return RedirectToAction(nameof(LichSuNapThe));
                                            case 3:
                                                ShowMessage("Nạp thẻ thất bại", $"Thẻ này đã tồn tại trên hệ thống!", "is-danger");
                                                break;
                                            default:
                                                ShowMessage("Nạp thẻ thất bại", $"!", "is-danger");
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    ShowMessage("Nạp thẻ thất bại", $"!", "is-danger");
                                }
                            }
                        }
                        else
                        {
                            ShowMessage("Nạp thẻ thất bại", $"Nguyên nhân do loại thẻ bạn chọn không nằm trong hệ thống!", "is-danger");
                        }
                    }
                    else
                    {
                        ShowMessage("Nạp thẻ thất bại", $"Nguyên nhân do mệnh giá {price_guest.ToString("###,###,###")} không tồn tại", "is-danger");
                    }
                }
                else
                {
                    ShowMessage("Nạp thẻ thất bại", $"Nguyên nhân do mệnh giá nạp là số âm ;)", "is-link");
                }
            }
            return RedirectToAction(nameof(NapThe));
        }

        public async Task<IActionResult> NapBank()
        {
            if (IfLoginIsNotOK(out IActionResult result))
            {
                return result;
            }
            UserData userData = GetUser(_context);
            if (userData == null) RedirectToHome();
            string nap = _context.LoadWebConfig(WebConfigModel.NỘI_DUNG_NẠP, "nap");
            List<BankQRModel> bankQRModels = new List<BankQRModel>();
            foreach (var item in _context.Banks.ToArray())
            {
                BankQRModel bankQRModel = new BankQRModel();
                bankQRModel.BankModel = item;
                bankQRModel.NapContent = $"{nap} {userData.username}";
                switch (item.Type)
                {
                    case BankModel.BankType.BANK:
                        bankQRModel.QRCode = $"https://img.vietqr.io/image/mbbank-{item.BankId}-compact2.jpg?amount=0&addInfo={bankQRModel.NapContent}&accountName={item.Name}";
                        break;
                    case BankModel.BankType.MOMO:
                        bankQRModel.QRCode = $"https://momosv3.apimienphi.com/api/QRCode?phone={item.BankId}&amount=20000&note={bankQRModel.NapContent}";
                        break;
                }
                bankQRModels.Add(bankQRModel);
            }
            return View(bankQRModels.ToArray());
        }


        public async Task<IActionResult> UserDetails()
        {
            if (IfLoginIsNotOK(out IActionResult result))
            {
                return result;
            }
            UserData userData = _context.Users.Where(x => x.user_id == this.HttpContext.Session.GetInt32(nameof(UserData.user_id))).FirstOrDefault();
            if (userData == null) return RedirectToHome();
            return View(new UserDetailModel(userData, _context.DongTiens.Where(x => x.UserName == userData.username).OrderByDescending(m => m.TimeCreate).ToArray()));
        }

        public async Task<IActionResult> Active()
        {
            if (IfLoginIsNotOK(out IActionResult result))
            {
                return result;
            }
            int price = _context.LoadWebConfig(WebConfigModel.ACTIVE_USER_PRICE, 10000);
            UserData userData = _context.Users.Where(x => x.user_id == this.HttpContext.Session.GetInt32(nameof(UserData.user_id))).FirstOrDefault();
            if (userData == null) return RedirectToHome();
            this.SetRole(userData);
            if (userData.coin >= price)
            {
                userData.coin -= price;
            }
            else
            {
                this.ShowMessage("Kích hoạt thất bại", $"Do số dư của bạn không đủ. Bạn còn thiếu {(price - userData.coin).ToString("###,###,###")} vnđ", "is-danger is-dark");
                return RedirectToAction(nameof(UserDetails));
            }
            userData.role = 1;
            this._context.SaveChanges();
            this.ShowMessage("Kích hoạt thành công", "Bây giờ bạn có thể đăng nhập vào trò chơi và tạo nhân vật", "is-success");
            return RedirectToAction(nameof(UserDetails));
        }
        [HttpGet]
        public IActionResult CallBackNapthe(
            [FromQuery] int status,
            [FromQuery] string code,
            [FromQuery] string serial,
            [FromQuery] int trans_id,
            [FromQuery] string telco,
            [FromQuery] string callback_sign,
            [FromQuery] string request_id,
            [FromQuery] string message,
            [FromQuery] int amount,
            [FromQuery] int card_value)
        {
            int percentField = _context.LoadWebConfig(WebConfigModel.TỈ_LỆ_NẠP, 100);
            float percent = (float)percentField / 100f;
            string partnerId = _context.LoadWebConfig(WebConfigModel.MÃ_ĐỐI_TÁC, string.Empty);
            string partnerKey = _context.LoadWebConfig(WebConfigModel.KEY_ĐỐI_TÁC, string.Empty);
            var sign = partnerKey + code + serial;
            var mysign = ComputeMd5Hash(sign);
            if (mysign == callback_sign)
            {
                CardModel card = _context.Cards.Where(x => x.UUID == request_id && x.Status == CardModel.CardStatus.Proccessing).FirstOrDefault();
                if (card != null)
                {
                    card.TimeUpdate = DateTime.Now;
                    card.TienGachThe = amount;
                    int roundCoin = (int)(amount * percent);
                    card.ThucNhan = roundCoin;
                    card.MenhGia = card_value;
                    switch (status)
                    {
                        case 1:
                            {
                                card.Status = CardModel.CardStatus.Success;
                                card.Note = "Đúng mệnh giá";
                            }
                            break;
                        case 2:
                            {
                                card.Status = CardModel.CardStatus.Success;
                                card.Note = "Sai mệnh giá";
                            }
                            break;
                        default:
                            card.Status = CardModel.CardStatus.Fail;
                            card.Note = message;
                            break;
                    }
                    if (card.Status == CardModel.CardStatus.Success)
                    {
                        UserData userData = _context.Users.Where(x => x.username == card.UserName).FirstOrDefault();
                        if (userData != null)
                        {
                            userData.coin += card.ThucNhan;
                            userData.tongnap += card.ThucNhan;
                            _context.DongTiens.Add(new DongTienModel()
                            {
                                Value = roundCoin,
                                UserName = userData.username,
                                NameSetDongTien = "Hệ thống gạch thẻ",
                                ValueBefore = userData.coin - roundCoin,
                                ValueAfter = userData.coin + roundCoin,
                                Content = "Hệ thống gạch thẻ duyệt nạp"
                            });
                        }
                    }
                }
            }

            _context.SaveChanges();
            _logger.LogError($"{nameof(CallBackNapthe)} [FromQuery] int status ={status},\r\n            [FromQuery] string code={code}, \r\n            [FromQuery] string serial={serial}, \r\n            [FromQuery] int trans_id ={trans_id},\r\n            [FromQuery] string telco={telco}, \r\n            [FromQuery] string callback_sign={callback_sign}, mySign ={mysign}, \r\n            [FromQuery] string request_id ={request_id}, \r\n            [FromQuery] string message={message}, \r\n            [FromQuery] int amount={amount}, \r\n            [FromQuery] int card_value={card_value}");
            return Json(new { IsSucces = true });
        }


        public async Task<JObject> ChargeCardAsync(string loaithe, string menhgia, string seri, string pin, string request_id, string partnerIdCard, string partnerKeyCard)
        {
            var POSTGET = new Dictionary<string, string>
             {
            { "request_id", request_id },
            { "code", pin },
            { "partner_id", partnerIdCard },
            { "serial", seri },
            { "telco", loaithe },
            { "command", "charging" }
             };
            var sortedPostGet = new SortedDictionary<string, string>(POSTGET);
            var sign = partnerKeyCard + pin + seri;
            var mysign = ComputeMd5Hash(sign);
            sortedPostGet.Add("amount", menhgia);
            sortedPostGet.Add("sign", mysign);
            var data = new FormUrlEncodedContent(sortedPostGet);

            using (var client = new HttpClient())
            {
                try
                {
                    var url = "https://thesieure.com/chargingws/v2";
                    var response = await client.PostAsync(url, data);
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(result);
                    return (JObject)JsonConvert.DeserializeObject(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    _logger.LogError(ex, "Lỗi ở hàm call thesieure");
                    return null;
                }
            }
        }

        private string ComputeMd5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public IActionResult LichSuNapThe()
        {
            if (IfLoginIsNotOK(out IActionResult result))
            {
                return result;
            }
            UserData userData = GetUser(_context);
            if (userData == null) RedirectToHome();
            return View(_context.Cards.Where(x => x.UserName == userData.username));
        }

        public IActionResult Enable2FA()
        {
            if (IfLoginIsNotOK(out IActionResult result))
            {
                return result;
            }
            UserData userData = GetUser(_context);
            if (userData == null) RedirectToHome();
            if (userData.secretKey != null)
            {
                ShowMessage("Lỗi", "Tài khoản của bạn đã kích hoạt 2FA rồi", "is-danger");
                return RedirectToHome();
            }
            if (this.GetSecretKey() == null)
            {
                this.SetSecretKey(Base32Encoding.ToString(KeyGeneration.GenerateRandomKey(20)));
            }
            return View(this.GetSecretKey() as object);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Enable2FAWithOTP(string secretKey, string verificationCode)
        {
            if (IfLoginIsNotOK(out IActionResult result))
            {
                return result;
            }
            UserData userData = GetUser(_context);
            if (userData == null || string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(verificationCode)) RedirectToHome();
            if (userData.secretKey != null)
            {
                ShowMessage("Lỗi", "Tài khoản của bạn đã kích hoạt 2FA rồi", "is-danger");
                return RedirectToHome();
            }
            var totp = new Totp(Base32Encoding.ToBytes(secretKey));
            if (totp.VerifyTotp(verificationCode, out var timeStep, new VerificationWindow(5, 5)))
            {
                userData.secretKey = secretKey;
                _context.SaveChanges();
                ShowMessage("Thành công", "Kích hoạt 2FA thành công", "is-success");
                return RedirectToAction(nameof(UserDetails));
            }
            ShowMessage("Lỗi", "Mã xác thực không chính xác hoặc quá hạn", "is-danger");
            return RedirectToAction(nameof(Enable2FA));
        }

        [HttpGet]
        public IActionResult QRCode2FA(string secretKey)
        {
            if (string.IsNullOrEmpty(secretKey))
            {
                return BadRequest();
            }
            UserData userData = GetUser(_context);
            if (userData == null) BadRequest();
            var uriString = new OtpUri(OtpType.Totp, secretKey, userData.username, "goPet");
            var qrCode = QrCode.EncodeText(uriString.ToString(), Ecc.Medium);
            byte[] svg = Encoding.UTF8.GetBytes(qrCode.ToSvgString(1));
            return new FileContentResult(svg, "image/svg+xml; charset=utf-8");
        }

        public IActionResult Disable2FA()
        {
            if (IfLoginIsNotOK(out IActionResult result))
            {
                return result;
            }
            UserData userData = GetUser(_context);
            if (userData == null) RedirectToHome();
            if (userData.secretKey == null)
            {
                ShowMessage("Lỗi", "Tài khoản của bạn chưa kích hoạt 2FA", "is-danger");
                return RedirectToHome();
            }
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Disable2FAWithOTP(string verificationCode)
        {
            if (string.IsNullOrEmpty(verificationCode) || verificationCode?.Length != 6)
            {
                ShowMessage("Lỗi", "Mã OTP phải đủ 6 số", "is-danger");
                return RedirectToAction(nameof(Disable2FA));
            }
            if (IfLoginIsNotOK(out IActionResult result))
            {
                return result;
            }
            UserData userData = GetUser(_context);
            if (userData == null) RedirectToHome();
            if (userData.secretKey == null)
            {
                ShowMessage("Lỗi", "Tài khoản của bạn chưa kích hoạt 2FA", "is-danger");
                return RedirectToAction(nameof(Disable2FA));
            }
            var totp = new Totp(Base32Encoding.ToBytes(userData.secretKey));
            if (totp.VerifyTotp(verificationCode, out var timeStep, new VerificationWindow(5, 5)))
            {
                userData.secretKey = null;
                _context.SaveChanges();
                ShowMessage("Thành công", "Tắt 2FA thành công", "is-success");
                return RedirectToAction(nameof(UserDetails));
            }
            ShowMessage("Lỗi", "Mã xác thực không chính xác hoặc quá hạn", "is-danger");
            return RedirectToAction(nameof(Disable2FA));
        }

        public IActionResult TwoFALogin()
        {
            if (IfLoginIsNotOK(out IActionResult result, out bool IsNeed2FA))
            {
                if (!IsNeed2FA)
                {
                    return result;
                }
            }
            UserData userData = GetUser(_context);
            if (userData == null) RedirectToHome();
            if (userData.secretKey == null)
            {
                ShowMessage("Lỗi", "Tài khoản của bạn chưa kích hoạt 2FA", "is-danger");
                return RedirectToHome();
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult TwoFALoginWithOtp(string verificationCode)
        {
            if (string.IsNullOrEmpty(verificationCode) || verificationCode?.Length != 6)
            {
                ShowMessage("Lỗi", "Mã OTP phải đủ 6 số", "is-danger");
                return RedirectToAction(nameof(Disable2FA));
            }
            bool isNeed2FA = false;
            IfLoginIsNotOK(out IActionResult result, out isNeed2FA);
            if (!isNeed2FA)
            {
                return RedirectToHome();
            }
            UserData userData = GetUser(_context);
            if (userData == null) RedirectToHome();
            if (userData.secretKey == null)
            {
                ShowMessage("Lỗi", "Tài khoản của bạn chưa kích hoạt 2FA", "is-danger");
                return RedirectToAction(nameof(UserDetails));
            }
            var totp = new Totp(Base32Encoding.ToBytes(userData.secretKey));
            if (totp.VerifyTotp(verificationCode, out var timeStep, new VerificationWindow(5, 5)))
            {
                SessionUtil.SetLogin2FAOK(this.HttpContext, userData);
                return RedirectToHome();
            }
            ShowMessage("Lỗi", "Mã xác thực không chính xác hoặc quá hạn", "is-danger");
            return RedirectToAction(nameof(TwoFALogin));
        }
    }
}
