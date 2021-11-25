using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BD2_projekt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BD2_projekt.Controllers
{
    public class LoginController : Controller
    {
        private readonly Context _db = new Context();
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(IFormCollection formCollection)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            byte[] encryptedPassword = sha1.ComputeHash(Encoding.ASCII.GetBytes(formCollection["password"]));
            String login = formCollection["login"];
            var query = from users in _db.Users where users.Password == encryptedPassword && users.Email == login select users.Email;
            int result = query.Count();
            if(result == 0)
            {
                Error error = new Error();
                error.PassError = "Podane dane logowania są niepoprawne";
                ViewData["errors"] = error;
                return View("Index");
            }
            else
            {
                String name = query.FirstOrDefault<String>();
                HttpContext.Session.SetString("user", name);
                SessionControl.SetUserType(HttpContext);
                //ViewData["session"] = HttpContext.Session.GetString("user");
                SessionControl.setViewData(_db, ViewData, HttpContext);
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.SetString("user", "");
            //ViewData["session"] = HttpContext.Session.GetString("user");
            SessionControl.setViewData(_db, ViewData, HttpContext);
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}