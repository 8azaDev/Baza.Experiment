using Baza.Experiment.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Baza.Experiment.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Content("Hell world");
        }

        // POST api/values
        [HttpPost]
        public IActionResult DateTimeTest([FromBody]DateTimeRequest request)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"原始数据:{request.Date}");
            sb.AppendLine($"UTC数据:{request.Date.ToUniversalTime()}");
            sb.AppendLine($"UTC String数据:{request.Date.ToUniversalTime().ToString("O")}");

            return Content(sb.ToString());
        }
    }
}
