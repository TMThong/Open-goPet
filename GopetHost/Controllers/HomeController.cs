using GopetHost.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GopetHost.Controllers
{
    public class HomeController : HelperController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Download()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult PageNotFound()
        {
            if (this.HttpContext.Items.ContainsKey("originalPath"))
            {
                _logger.LogDebug($"Page not found with path = {this.HttpContext.Items["originalPath"]}");
                ShowMessage("Đường dẫn không tồn tại", $"Đường dẫn truy cập vào {this.HttpContext.Items["originalPath"]} không tồn tại. Có vẻ như đây có thể là đường dẫn của website cũ.", "is-danger");
            }
            return View();
        }
    }
}
