using Microsoft.AspNetCore.Mvc;
using MyStore01.WebUI.Models;
using MyStore01.WebUI.Models.Users_Info;

namespace MyStore01.WebUI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Signup(SignupModel model)
        {
            if (ModelState.IsValid) 
            {
                return View();
            }
            Appuser user = new Appuser { Email = model.Email,UserName = model.Name};
            return View();
        }
    }
}
