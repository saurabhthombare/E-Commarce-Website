using Microsoft.AspNetCore.Mvc;

namespace E_Commarce_Website.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
