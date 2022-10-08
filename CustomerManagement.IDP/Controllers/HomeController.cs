using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.IDP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}