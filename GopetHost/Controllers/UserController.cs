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

namespace GopetHost.Controllers
{
    public class UserController : HelperController
    {
        private readonly AppDatabaseContext _context;

        public UserController(AppDatabaseContext context)
        {
            _context = context;
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
                    this.SetLoginOK(userData);
                    ShowMessage("Đăng nhập thành công", "Đăng nhập thành công mời bạn thao tác!!!", "is-success");
                    goto TO_HOME;
                }
                else
                {
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

        public async Task<IActionResult> NapTheCao()
        {
            if (IfLoginIsNotOK(out IActionResult result))
            {
                return result;
            }
            return View();
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
            return View(userData);
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
    }
}
