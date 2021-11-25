using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BD2_projekt.Controllers
{
    public class ShoppingHistoryController : Controller
    {
        Context _db = new Context();
        [HttpGet]
        public IActionResult Index()
        {
            Invoices[] invoices = (from inv in _db.Invoices.Include(x => x.OrderUnit).ThenInclude(y => y.Product)
                                   where inv.Customer.Email == HttpContext.Session.GetString("user")
                                   select inv).ToArray();
            ViewData["invoices"] = invoices;
            SessionControl.setViewData(_db, ViewData, HttpContext);
            return View();
        }
    }
}