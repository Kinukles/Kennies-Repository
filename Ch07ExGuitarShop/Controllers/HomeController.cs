using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult About()
        {
            return View();
        }
        [Route("ContactUs")]
        public IActionResult ContactUs()
        {
            var contact = new Dictionary<string, string>
            {
                ["Email"] = "info@myguitarshop.com",
                ["Phone"] = "(555) 123-4567",
            };
            return View(contact);
        }
    }
}
