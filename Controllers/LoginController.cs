using Microsoft.AspNetCore.Mvc;

namespace KTRS.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            // Basit bir kimlik doðrulama örneði
            if (username == "yetkili" && password == "yetkili123")
            {
                return RedirectToAction("Index", "Yetkili");
            }
            else if (username == "ogrenci" && password == "ogrenci123")
            {
                return RedirectToAction("Index", "Ogrenci");
            }
            else
            {
                ViewBag.Error = "Geçersiz kullanýcý adý veya þifre";
                return View();
            }
        }
    }
}

