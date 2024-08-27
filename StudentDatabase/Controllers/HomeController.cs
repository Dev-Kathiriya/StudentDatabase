using Microsoft.AspNetCore.Mvc;
using StudentDatabase.BAL;
using StudentDatabase.Controllers.DAL;
using StudentDatabase.Models;

namespace StudentDatabase.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _configuration;
        public HomeController(IConfiguration configuration) => _configuration = configuration;
        public IActionResult Login() => View();
        public IActionResult SignUp() => View();
        private User_DalBase UserDalObj() => new User_DalBase(this._configuration.GetConnectionString("ConnectionString"));

        public IActionResult Registered(User user)
        {
            if (TryValidateModel(user)) return View("SignUp", user);
            (UserDalObj()).InsertUser(user);
            return RedirectToAction("Login");
        }
        [HttpPost]
        public IActionResult checkLoginDetails(User user)
        {
            if (!TryValidateModel(user)) return View("Login", user);
            user = UserDalObj().SelectUser(user);
            if (!(user != null && user.UserId > 0))
            {
                TempData["error"] = "Username or Password is incorrect";
                return View("Login", user);
            }
            _SetSession(user);
            int? a = UserSessonCV.UserId();
            if (HttpContext.Session.GetString("Username") != null && HttpContext.Session.GetString("UserPassword") != null)
                return RedirectToAction("Table", "LOC_Country", new { area = "LOC_Country" });
            return View("Login");
        }
        private void _SetSession(User user)
        {
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("UserPassword", user!.UserPassword);
            HttpContext.Session.SetString("Email", user.Email);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }
    }
}