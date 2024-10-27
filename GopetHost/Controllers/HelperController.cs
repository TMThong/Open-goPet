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
    }
}
