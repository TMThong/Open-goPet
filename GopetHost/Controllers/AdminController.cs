using GopetHost.Data;
using GopetHost.Models;
using GopetHost.Ulti;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System;

namespace GopetHost.Controllers
{
    public class AdminController : HelperController
    {
        public AppDatabaseContext _context;

        public AdminController(AppDatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (ReturnIfNonAdmin(out IActionResult actionResult))
            {
                return actionResult;
            }
            return View();
        }

        public bool ReturnIfNonAdmin(out IActionResult actionResult)
        {
            actionResult = null;
            UserData userData = GetUser(_context);
            if (userData == null)
            {
                actionResult = RedirectToHome();
                return true;
            }
            else if (!userData.IsAdmin)
            {
                actionResult = RedirectToHome();
                return true;
            }
            return false;
        }

        public IActionResult Setting()
        {
            if (ReturnIfNonAdmin(out IActionResult actionResult))
            {
                return actionResult;
            }
            return View(this._context.WebConfigs.ToArray());
        }

        public IActionResult EditSetting(string Key, string Value)
        {
            if (ReturnIfNonAdmin(out IActionResult actionResult))
            {
                return actionResult;
            }
            var config = _context.WebConfigs.Where(x => x.Key == Key).FirstOrDefault();
            if (config != null)
            {
                config.Value = Value;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Setting));
        }

        public IActionResult AddMomo()
        {
            if (ReturnIfNonAdmin(out IActionResult actionResult))
            {
                return actionResult;
            }
            return View();
        }
        public IActionResult AddMomoModel([Bind("BankId,Name,Logo,Token")] BankModel bankModel)
        {
            bankModel.Type = BankModel.BankType.MOMO;
            if (ReturnIfNonAdmin(out IActionResult actionResult))
            {
                return actionResult;
            }
            if (ModelState.IsValid)
            {
                _context.Banks.Add(bankModel);
                _context.SaveChanges();
                ShowMessage("Thêm thành công", "Bạn vui lòng chú ý trạng thái tài khoản sau khi thêm nhé.", "is-success");
            }
            else
            {
                ShowMessage("Không thể thêm", "Do bạn không điền đủ các trường thông tin.", "is-danger");
                return RedirectToAction(nameof(AddMomo));
            }
            return RedirectToAction(nameof(ManagerMomo));
        }

        public IActionResult RemoveMomoModel(int Id)
        {
            if (ReturnIfNonAdmin(out IActionResult actionResult))
            {
                return actionResult;
            }
            _context.Banks.Remove(_context.Banks.Where(x => x.Id == Id).FirstOrDefault());
            _context.SaveChanges();
            return RedirectToAction(nameof(ManagerMomo));
        }

        public IActionResult RemoveATMModel(int Id)
        {
            if (ReturnIfNonAdmin(out IActionResult actionResult))
            {
                return actionResult;
            }
            _context.Banks.Remove(_context.Banks.Where(x => x.Id == Id).FirstOrDefault());
            _context.SaveChanges();
            return RedirectToAction(nameof(ManagerATM));
        }

        public IActionResult ManagerMomo()
        {
            if (ReturnIfNonAdmin(out IActionResult actionResult))
            {
                return actionResult;
            }
            return View(_context.Banks.Where(x => x.Type == BankModel.BankType.MOMO));
        }

        public IActionResult HistoryMomo()
        {
            if (ReturnIfNonAdmin(out IActionResult actionResult))
            {
                return actionResult;
            }
            return View(_context.MomoTranslations.ToArray());
        }

        public IActionResult AddATM()
        {
            if (ReturnIfNonAdmin(out IActionResult actionResult))
            {
                return actionResult;
            }
            return View();
        }

        public IActionResult AddATMModel([Bind("BankId,Name,Logo,Token,Password")] BankModel bankModel)
        {
            bankModel.Type = BankModel.BankType.BANK;
            if (ReturnIfNonAdmin(out IActionResult actionResult))
            {
                return actionResult;
            }
            if (ModelState.IsValid)
            {
                _context.Banks.Add(bankModel);
                _context.SaveChanges();
                ShowMessage("Thêm thành công", "Bạn vui lòng chú ý trạng thái tài khoản sau khi thêm nhé.", "is-success");
            }
            else
            {
                ShowMessage("Không thể thêm", "Do bạn không điền đủ các trường thông tin.", "is-danger");
                return RedirectToAction(nameof(AddMomo));
            }
            return RedirectToAction(nameof(ManagerATM));
        }

        public IActionResult ManagerATM()
        {
            if (ReturnIfNonAdmin(out IActionResult actionResult))
            {
                return actionResult;
            }
            return View(_context.Banks.Where(x => x.Type == BankModel.BankType.BANK));
        }

        public IActionResult HistoryATM()
        {
            if (ReturnIfNonAdmin(out IActionResult actionResult))
            {
                return actionResult;
            }
            return View(_context.BankTranslations.ToArray());
        }

        public IActionResult AddNewPost()
        {
            if (ReturnIfNonAdmin(out IActionResult actionResult))
            {
                return actionResult;
            }
            return View(_context.Tags.ToArray());
        }

        public IActionResult AddPost(string Title, string Description, string Username, string Tags)
        {
            if (ReturnIfNonAdmin(out IActionResult actionResult))
            {
                return actionResult;
            }

            if (string.IsNullOrEmpty(Tags))
            {
                ShowMessage("Thẻ rỗng", "Vui lòng thêm ít nhất 1 thẻ", "is-danger");
                return RedirectToAction(nameof(AddNewPost));
            }

            string[] p = Tags.Split(',');
            if (p.Length > 0)
            {
                PostModel model = new PostModel();
                model.Title = Title;
                model.Description = Description;
                model.Username = Username;
                _context.Posts.Add(model);
                _context.SaveChanges();
                foreach (var item in _context.Tags.ToArray().Where(x => p.Any(m => m.Trim() == x.Tag)))
                {
                    PostTagModel itemModel = new PostTagModel()
                    {
                        PostId = model.Id,
                        TagId = item.Id,
                    };
                    _context.PostTags.Add(itemModel);
                }
                _context.SaveChanges();
            }

            return RedirectToHome();
        }

        public IActionResult ManagerPost()
        {
            if (ReturnIfNonAdmin(out IActionResult actionResult))
            {
                return actionResult;
            }
            return View(_context.Posts.ToArray());
        }

        public IActionResult User()
        {
            if (ReturnIfNonAdmin(out IActionResult actionResult))
            {
                return actionResult;
            }
            return View(_context.Users.ToArray());
        }

        public IActionResult EditUser(int user_id, string password, int coin, string phone, string email)
        {
            if (ReturnIfNonAdmin(out IActionResult actionResult))
            {
                return actionResult;
            }
            UserData userData = _context.Users.Where(x => x.user_id == user_id).FirstOrDefault();
            if (userData != null)
            {
                userData.password = password;
                userData.coin = coin;
                userData.phone = phone;
                userData.email = email;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(User));
        }

        public IActionResult HistoryCard()
        {
            if (ReturnIfNonAdmin(out IActionResult actionResult))
            {
                return actionResult;
            }
            return View(_context.Cards.ToArray());
        }
        [HttpGet]
        public IActionResult GetUserPage([FromQuery] IDictionary<string, string> search, int start, int length, int? draw)
        {
            if (this.IsLoginOK())
            {
                string searchValue = search.ContainsKey("value") ? search["value"] : null;
                var listUser = _context.Users.Where(x => searchValue == null || x.username.Contains(searchValue));
                var user = listUser.Skip(start).Take(length); 
                var datas = new List<object>();

                foreach (var item in user)
                {
                    datas.Add(new
                    {
                        id = item.user_id,
                        username = item.username,
                        password = item.password,
                        coin = item.coin,
                        phone = item.phone,
                        email = item.email,
                        create_date = item.create_date,
                        tongnap = item.tongnap,
                        action = item.user_id
                    });
                }

                return Json(
                    new
                    {
                        data = datas,
                        total = length,
                        draw = draw,
                        recordsTotal = 10,
                        recordsFiltered = listUser.Count()
                    });
            }
            return Json(
                new
                {
                    data = new object[] {
                        new { id = 1, username = "1", password = "1", coin = "1", phone = "1", email = "1", create_date = "1", tongnap = "1", action = "1", }
                    },
                    total = length,
                    draw = draw,
                    recordsTotal = 10,
                    recordsFiltered = 1100
                });
        }
    }
}
