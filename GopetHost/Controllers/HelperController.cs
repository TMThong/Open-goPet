using GopetHost.Ulti;
using Microsoft.AspNetCore.Mvc;

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
    }
}
