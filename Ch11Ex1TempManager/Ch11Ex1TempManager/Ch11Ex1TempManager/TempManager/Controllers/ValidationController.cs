namespace TempManager.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TempManager.Models;

    public class ValidationController : Controller
    {
        private TempManagerContext context { get; }

        public ValidationController(TempManagerContext ctx)
        {
            context = ctx;
        }

        public JsonResult CheckDate(string date)
        {
            DateTime tempDate = Convert.ToDateTime(date);
            Temp? temp = context.Temps
                .FirstOrDefault(t => t.Date == tempDate);

            if (temp == null)
            {
                return Json(true);
            }
            else
            {
                return Json("That date is already in the database.");
            }
        }
    }
}