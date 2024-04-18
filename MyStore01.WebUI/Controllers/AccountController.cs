using Microsoft.AspNetCore.Mvc;

namespace MyStore01.WebUI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
