using GopetHost.Data;
using GopetHost.Models;
using GopetHost.Ulti;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GopetHost.Controllers
{
    public class HelperController : Controller
    {
        public void ShowMessage(string title, string message, string attrs)
        {
            TempData[TempDataUtil.MESSAGE_TEMP_TITLE] = title;
            TempData[TempDataUtil.MESSAGE_TEMP] = message;
            TempData[TempDataUtil.MESSAGE_TEMP_CLASS_ATT] = attrs;
        }

        public IActionResult RedirectToHome()
        {
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public bool IfLoginIsNotOK(out IActionResult result)
        {
            return IfLoginIsNotOK(out result, out _);
        }

        public bool IfLoginIsNotOK(out IActionResult result, out bool isNeed2FA)
        {
            result = null;
            isNeed2FA = false;
            if (this.IsLoginOK())
            {
                isNeed2FA = SessionUtil.IsNeedLogin2FA(this.HttpContext);
                if (isNeed2FA)
                {
                    result = RedirectToAction(nameof(UserController.TwoFALogin), "User");
                    return true;
                }
                return false;
            }
            ShowMessage("Không thể dùng chức năng này", "Do bạn chưa đăng nhập nên không thể sử dụng chức năng này!", "is-danger");
            result = RedirectToHome();
            return true;
        }

        public UserData GetUser(AppDatabaseContext _context)
        {
            UserData userData = _context.Users.Where(x => x.user_id == this.HttpContext.Session.GetInt32(nameof(UserData.user_id))).FirstOrDefault();
            return userData;
        }


        public UserData GetUser(AppDatabaseContext _context, string username)
        {
            UserData userData = _context.Users.Where(x => x.username == username).FirstOrDefault();
            return userData;
        }
    }
}
