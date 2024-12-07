using Microsoft.AspNetCore.Mvc;

namespace StudentManagement.Areas.Admin.Controllers
{
    public class Dashboard : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
