using Baza.Experiment.Models;
using Microsoft.AspNetCore.Mvc;
using Nest;
using System;
using System.Text;

namespace Baza.Experiment.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var sb = new StringBuilder();
            foreach (var item in TimeZoneInfo.GetSystemTimeZones())
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
            var date = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            //var date = DateTime.SpecifyKind(DateTime.Parse("2018-02-02"), DateTimeKind.Local);
            for (var i = 0; i < 10; i++)
            {
                var temp = DateTime.Now.AddMinutes(i);
                client.Index(new EsItem
                {
                    Name = i.ToString(),
                    Date = temp,
                    Ticks = temp.Ticks
                }, idx => idx.Index("mytestindex"));
            }
            return Content("");
        }

        public ActionResult Search()
        {
            var node = new Uri("http://ipv4.fiddler:9200");
            var settings = new ConnectionSettings(node).DefaultMappingFor<EsItem>(i => i
                .IndexName("mytestindex")
                .TypeName("esitem")
            );
            var client = new ElasticClient(settings);
            var request = new SearchRequest
            {
                From = 0,
                Size = 100,
                Query = new DateRangeQuery()
                {
                    Field = "esitem.date",
                    Boost = 0,
                    GreaterThanOrEqualTo = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified)
                }
            };

            var response = client.Search<EsItem>(s => s.Query(q => q.DateRange(r => r.Field(i => i.Date).GreaterThanOrEquals(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified)))));
            return Json(response);
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
