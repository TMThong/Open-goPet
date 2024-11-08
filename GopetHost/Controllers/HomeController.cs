using GopetHost.Data;
using GopetHost.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GopetHost.Controllers
{
    public class HomeController : HelperController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDatabaseContext _context;

        public HomeController(ILogger<HomeController> logger, AppDatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index([FromQuery] int Page = 0, [FromQuery] int? TagId = null)
        {
            Page = Math.Max(Page, 0);
            int numpage = _context.LoadWebConfig<int>(WebConfigModel.SỐ_TRANG_MÀ_DIỄN_ĐÀN_HIỂN_THỊ, 0);
            IndexHomeBagModel model = new IndexHomeBagModel();
            model.CurrentPage = Page;
            model.MaxPage = this._context.Posts.Count() / numpage;
            model.Tags = _context.Tags.ToArray();
            model.TagId = TagId;
            if (TagId.HasValue)
            {
                model.MaxPage = this._context.Posts.OrderByDescending(x => x.TimeCreate).Where(x => x.Tags.Any(x => x.TagId == TagId.Value)).Count() / numpage;
            }
            if (model.MaxPage < Page)
            {
                return Index(0);
            }
            if (TagId.HasValue)
            {
                model.Posts = this._context.Posts.OrderByDescending(x => x.TimeCreate).Where(x => x.Tags.Any(x => x.TagId == TagId.Value)).Skip(model.CurrentPage * numpage).ToArray();
            }
            else
            {
                model.Posts = this._context.Posts.OrderByDescending(x => x.TimeCreate).Skip(model.CurrentPage * numpage).ToArray();
            }

            foreach (var item in model.Posts)
            {
                item.Tags = _context.PostTags.Where(m => m.PostId == item.Id).ToArray();

                foreach (var item1 in item.Tags)
                {
                    item1.Tag = _context.Tags.Where(x => x.Id == item1.TagId).FirstOrDefault();
                }
            }

            return View(model);
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

        public IActionResult Post(int Id)
        {
            var post = _context.Posts.Where(x => x.Id == Id).FirstOrDefault();
            if (post == null)
            {
                return RedirectToHome();
            }
            post.Views++;
            _context.SaveChanges();
            return View(post);
        }
    }
}
