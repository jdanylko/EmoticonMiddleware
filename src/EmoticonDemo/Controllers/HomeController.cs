using EmoticonDemo.ActionFilter;
using Microsoft.AspNetCore.Mvc;

namespace EmoticonDemo.Controllers
{
    public class HomeController : Controller
    {
        // [EmoticonFilter]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
