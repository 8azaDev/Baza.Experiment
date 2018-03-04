using Baza.Experiment.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using Nest;

namespace Baza.Experiment.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var sb = new StringBuilder();
            foreach(var item in TimeZoneInfo.GetSystemTimeZones())
            {
                sb.AppendLine($"StandardName:{item.StandardName}, Id: {item.Id}");
            }
            return Content(sb.ToString());
        }

        public ActionResult Create(DateTimeRequest request)
        {
            var node = new Uri("http://127.0.0.1:9200");
            var settings = new ConnectionSettings(node);
            var client = new ElasticClient(settings);
            //var date = DateTime.SpecifyKind(DateTime.Parse("2018-02-02"), DateTimeKind.Local);
            for (var i = 0; i < 10; i++)
            {
                client.Index(new
                {
                    Name = i,
                    Date = request.Date.AddMinutes(i)
                }, idx => idx.Index("mytestindex"));
            }
            return Content("");
        }

        // POST api/values
        [HttpPost]
        public IActionResult DateTimeTest(DateTimeRequest request)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"原始数据:{request.Date}");
            sb.AppendLine($"UTC数据:{request.Date.ToUniversalTime()}");
            sb.AppendLine($"UTC String数据:{request.Date.ToString("O")}");
            //sb.AppendLine($"Local Now:{DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local).ToUniversalTime().ToString("O")}");
            //sb.AppendLine($"Unspecified Now:{DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).ToUniversalTime().ToString("O")}");
            //sb.AppendLine($"Utc Now:{DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc).ToUniversalTime().ToString("O")}");

            return Content(sb.ToString());
        }
    }
}
