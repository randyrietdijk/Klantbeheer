using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Customers");
        }
    }
}