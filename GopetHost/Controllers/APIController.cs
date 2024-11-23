using GopetHost.Attributes;
using GopetHost.Data;
using GopetHost.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Numerics;
using System.Text.RegularExpressions;

namespace GopetHost.Controllers
{
    public class APIController : HelperController
    {
        public string Pattern = @"\bnap\b\s+(?<username>[a-zA-Z0-9_]+)\b";

        public AppDatabaseContext _context { get; }

        public ILogger<APIController> _logger;

        public APIController(AppDatabaseContext context, ILogger<APIController> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            return Json(new { Data = 1 });
        }
        //[IpRestrict("160.30.160.83")]
        [Route("THAIANCUC/ABC/MOMO")]
        public IActionResult AutoMomo()
        {
            int percentField = _context.LoadWebConfig(WebConfigModel.TỈ_LỆ_NẠP, 100);
            float percent = (float)percentField / 100f;
            WebClient webClient = new WebClient();
            foreach (var item in _context.Banks.Where(x => x.Type == Models.BankModel.BankType.MOMO))
            {
                try
                {
                    string json = webClient.DownloadString(item.URL_API);
                    JObject jsonObject = JObject.Parse(json);
                    if (jsonObject.ContainsKey("momoMsg"))
                    {
                        var tranList = jsonObject["momoMsg"]["tranList"];
                        foreach (JObject tranData in tranList)
                        {
                            MomoTranslationModel momoTranslationModel = new MomoTranslationModel();
                            momoTranslationModel.TranId = tranData["tranId"].ToString();
                            momoTranslationModel.Username = "Không tìm thấy";
                            momoTranslationModel.PartnerName = tranData["partnerName"].ToString();
                            momoTranslationModel.PartnerId = tranData["partnerId"].ToString();
                            momoTranslationModel.Amount = int.Parse(tranData["amount"].ToString());
                            momoTranslationModel.Comment = tranData["comment"].ToString();
                            long milliseconds = long.Parse(tranData["clientTime"].ToString());
                            DateTime dateTime = new DateTime(1970, 1, 1).AddMilliseconds(milliseconds);
                            momoTranslationModel.TimeCharge = dateTime;
                            Match match = Regex.Match(momoTranslationModel.Comment, Pattern);
                            if (match.Success)
                            {
                                string username = match.Groups["username"].Value;
                                momoTranslationModel.Username = username;
                            }
                            UserData userData = GetUser(_context, momoTranslationModel.Username);
                            if (tranData.ContainsKey("extra"))
                            {
                                var extra = JObject.Parse(tranData["extra"].ToString());
                                if (extra.ContainsKey("partnerAvatarUrl"))
                                {
                                    momoTranslationModel.PartnerAvatarUrl = extra["partnerAvatarUrl"].ToString();
                                }
                            }
                            if (!_context.MomoTranslations.Any(m => m.TranId == momoTranslationModel.TranId))
                            {
                                _context.MomoTranslations.Add(momoTranslationModel);
                                if (userData != null)
                                {
                                    int roundCoin = (int)(momoTranslationModel.Amount * percent);
                                    userData.coin += roundCoin;
                                    userData.tongnap += roundCoin;
                                    momoTranslationModel.AmountReceived = roundCoin;
                                    momoTranslationModel.IsAddCoin = true;
                                    _context.DongTiens.Add(new DongTienModel()
                                    {
                                        Value = roundCoin,
                                        UserName = userData.username,
                                        NameSetDongTien = "Hệ thống Momo",
                                        ValueBefore = userData.coin - roundCoin,
                                        ValueAfter = userData.coin + roundCoin,
                                        Content = "Hệ thống Momo duyệt nạp"
                                    });
                                }
                            }
                        }
                    }
                    else if (jsonObject.ContainsKey("msg"))
                    {
						item.Message = jsonObject["msg"].ToString();
						item.MessageAttrs = "is-danger is-light";
					}
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "AutoMomo Exception");
                }
            }
            _context.SaveChanges();
            return Json(new { Data = 1 });
        }
        //[IpRestrict("160.30.160.83")]
        [Route("THAIANCUC/ABC/ATM")]
        public IActionResult AutoATM()
        {
            int percentField = _context.LoadWebConfig(WebConfigModel.TỈ_LỆ_NẠP, 100);
            float percent = (float)percentField / 100f;
            WebClient webClient = new WebClient();
            foreach (var item in _context.Banks.Where(x => x.Type == Models.BankModel.BankType.BANK))
            {
                try
                {
                    string json = webClient.DownloadString(item.URL_API);
                    JObject jsonObject = JObject.Parse(json);
                    if (jsonObject.ContainsKey("data"))
                    {
                        var tranList = jsonObject["data"];
                        foreach (JObject tranData in tranList)
                        {
                            BankTranslationModel bankTranslationModel = new BankTranslationModel();
                            bankTranslationModel.Amount = int.Parse(tranData["creditAmount"].ToString());
                            bankTranslationModel.TranId = tranData["refNo"].ToString();
                            bankTranslationModel.Comment = tranData["description"].ToString();
                            bankTranslationModel.UserName = "Không tìm thấy";
                            Match match = Regex.Match(bankTranslationModel.Comment, Pattern);
                            if (match.Success)
                            {
                                string username = match.Groups["username"].Value;
                                bankTranslationModel.UserName = username;
                            }
                            UserData userData = GetUser(_context, bankTranslationModel.UserName);
                            if (!_context.BankTranslations.Any(m => m.TranId == bankTranslationModel.TranId))
                            {
                                _context.BankTranslations.Add(bankTranslationModel);
                                if (userData != null)
                                {
                                    int roundCoin = (int)(bankTranslationModel.Amount * percent);
                                    userData.coin += roundCoin;
                                    userData.tongnap += roundCoin;
                                    bankTranslationModel.AmountReceived = roundCoin;
                                    bankTranslationModel.IsAddCoin = true;
                                    _context.DongTiens.Add(new DongTienModel()
                                    {
                                        Value = roundCoin,
                                        UserName = userData.username,
                                        NameSetDongTien = "Hệ thống ATM",
                                        ValueBefore = userData.coin - roundCoin,
                                        ValueAfter = userData.coin + roundCoin,
                                        Content = "Hệ thống ATM duyệt nạp"
                                    });
                                }
                            }
                        }
                    }
                    else if (jsonObject.ContainsKey("status"))
                    {
                        item.Message = jsonObject["msg"].ToString();
                        item.MessageAttrs = "is-danger is-light";
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "AutoATM Exception");
                }
            }
            _context.SaveChanges();
            return Json(new { Data = 1 });
        }
    }
}
