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
            result = null;
            if (this.IsLoginOK())
            {
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
