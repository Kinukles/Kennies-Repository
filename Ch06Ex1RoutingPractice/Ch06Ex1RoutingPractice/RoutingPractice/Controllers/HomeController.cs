using Microsoft.AspNetCore.Mvc;

namespace RoutingPractice.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Home");
        }

        public IActionResult Privacy()
        {
            return Content("Privacy");
        }
        [Route("[action]/{start}/{end?}/{message?}")]
        public IActionResult Countdown(int start, int end = 0, string message = "")
        {
            if (start < 0) return BadRequest("Start must be >= 0");

            string contentString = "Counting down:\n";
            for (int i = start; i >= end; i--)
            {
                contentString += i + "\n";
            }

            contentString += message;

            return Content(contentString);
        }
        public IActionResult Display(string id)
        {
            if (id == null) {
                return Content("No ID supplied.");
            }
            else {
                return Content("ID: " + id);

            }
        }
    }
}