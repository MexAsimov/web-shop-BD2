using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BD2_projekt.Controllers
{
    public class BuyController : Controller
    {
        Context _db = new Context();
        [HttpGet]
        public IActionResult Index()
        {
            Cart cart = (from c in _db.Cart.Include(x => x.CartElements).ThenInclude(y => y.Product).Include("User") where c.User.Email == HttpContext.Session.GetString("user") select c).FirstOrDefault();

            SessionControl.setViewData(_db, ViewData, HttpContext);
            ViewData["success"] = false;
            ViewData["cart"] = cart;
            return View();
        }

        [HttpPost]
        public IActionResult Buy()
        {
            Cart cart = (from c in _db.Cart.Include(x => x.CartElements).ThenInclude(y => y.Product).Include("User") where c.User.Email == HttpContext.Session.GetString("user") select c).FirstOrDefault();
            Console.WriteLine(cart.CartElements.ToArray()[0].Product.ProductName);
            Invoices invoice = new Invoices();
            invoice.Customer = (from c in _db.Customers where c.Email == HttpContext.Session.GetString("user") select c).FirstOrDefault();
            invoice.InvoiceDate = DateTime.Now;
            invoice.OrderUnit = new List<OrderUnit>();
            foreach(CartElement cartElement in cart.CartElements)
            {
                OrderUnit orderUnit = new OrderUnit();
                orderUnit.Product = cartElement.Product;
                orderUnit.unitPrice = cartElement.Product.price;
                orderUnit.NumberOfProducts = cartElement.NumberOfProducts;
                invoice.OrderUnit.Add(orderUnit);
                cartElement.NumberOfProducts = 0;
            }
            _db.Invoices.Add(invoice);
            _db.SaveChanges();
            SessionControl.setViewData(_db, ViewData, HttpContext);
            ViewData["cart"] = cart;
            ViewData["success"] = true;
            return View("Index");
        }

    }
}