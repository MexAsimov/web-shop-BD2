using System;
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
    public class RegistrationController : Controller
    {
        private readonly Context _db = new Context();
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(IFormCollection collection)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            byte[] encryptedPassword = sha1.ComputeHash(Encoding.ASCII.GetBytes(collection["password"]));
            Error passError = new Error();
            if(collection["password"].ToString().Length == 0)
            {
                passError.PassError = "hasło musi zostać podane";
                ViewData["errors"] = passError;
                SessionControl.setViewData(_db, ViewData, HttpContext);
                return View("Index");
            }
            if(collection["password"] != collection["password2"])
            {
                passError.PassError = "hasła muszą być takie same";
                ViewData["errors"] = passError;
                SessionControl.setViewData(_db, ViewData, HttpContext);

                return View("Index");
            }
            if (collection["type"] == "customer")
            {
                Customers user = new Customers();
                user.FirstName = collection["customer-first-name"];
                user.LastName = collection["customer-last-name"];
                user.CardNumber = collection["customer-card-number"];
                user.City = collection["city"];
                user.Country = collection["state"];
                user.Email = collection["email"];
                user.NIP = collection["NIP"];
                user.Number = collection["number"];
                user.PostCode = collection["post-code"];
                user.Street = collection["street"];
                user.Phone = collection["phone"];
                user.Password = encryptedPassword;
                _db.Customers.Add(user);
                _db.SaveChanges();
            }
            else
            {
                Distributors user = new Distributors();
                user.CompanyName = collection["distributor-company-name"];
                user.BankAccountNumber = collection["distributor-bank-account"];
                user.City = collection["city"];
                user.Country = collection["state"];
                user.Email = collection["email"];
                user.NIP = collection["NIP"];
                user.Number = collection["number"];
                user.PostCode = collection["post-code"];
                user.Street = collection["street"];
                user.Phone = collection["phone"];
                user.Password = encryptedPassword;
                _db.Distributors.Add(user);
                _db.SaveChanges();
            }
            SessionControl.setViewData(_db, ViewData, HttpContext);
            return View("Index");
        }
    }
}