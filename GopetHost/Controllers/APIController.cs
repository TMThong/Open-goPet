using GopetHost.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;

namespace GopetHost.Controllers
{
    [Route("/__/__/__/API")]
    public class APIController : Controller
    {
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

        public IActionResult AutoMomo()
        {
            WebClient webClient = new WebClient();
            foreach (var item in _context.Banks.Where(x => x.Type == Models.BankModel.BankType.MOMO))
            {
                try
                {
                    string json = webClient.DownloadString(item.URL_API);
                    JObject jsonObject = JObject.Parse(json);
                    if (jsonObject.ContainsKey("momoMsg"))
                    {
                        var tranList = jsonObject["momoMsg"]["tranList"].Values();
                        foreach (var tranData in tranList)
                        {

                        }
                    }
                    else if (jsonObject.ContainsKey("status"))
                    {
                        if (jsonObject["status"].Equals(99))
                        {
                            item.Message = jsonObject["msg"].ToString();
                            item.MessageAttrs = "is-danger is-light";
                        }
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

        public IActionResult AutoATM()
        {
            return Json(new { Data = 1 });
        }
    }
}
