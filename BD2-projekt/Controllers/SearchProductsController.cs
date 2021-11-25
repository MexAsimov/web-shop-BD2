using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BD2_projekt.Controllers
{
    [EnableCors]
    public class SearchProductsController : Controller
    {
        Context _db = new Context();
        [HttpGet]
        public IActionResult Index()
        {
            SessionControl.setViewData(_db, ViewData, HttpContext);
            return View();
        }

        [HttpGet]
        public IActionResult getCartData()
        {
            Cart cart = (from c in _db.Cart.Include(x => x.CartElements).ThenInclude(y => y.Product).Include("User") where c.User.Email == HttpContext.Session.GetString("user") select c).FirstOrDefault();
            return Json(cart);
        }

        [HttpPost]
        public IActionResult AddOrRemove([FromBody] JSONAddRemoveToCart request)
        {
            int ID = request.ID;
            Cart cart = (from c in _db.Cart.Include("CartElements") where c.User.Email == HttpContext.Session.GetString("user") select c).FirstOrDefault<Cart>();
            Products product = (from p in _db.Products where p.ProductsID == ID select p).FirstOrDefault<Products>();
            if (cart == null)
            {
                cart = new Cart();
                Users currentUser = (from u in _db.Users where u.Email == HttpContext.Session.GetString("user") select u).FirstOrDefault<Users>();
                cart.User = currentUser;
                cart.CartElements = new List<CartElement>();
                _db.Cart.Add(cart);
            }
            if(cart.CartElements == null)
            {
                cart.CartElements = new List<CartElement>();
            }
            if (cart.CartElements.Where(x => x.Product != null && x.Product.ProductsID == product.ProductsID).ToList().Count() == 0)
            {
                CartElement cartElement = new CartElement();
                cartElement.Product = product;
                cartElement.NumberOfProducts = 0;
                cart.CartElements.Add(cartElement);
            }
            if (request.add)
            {
                cart.CartElements.Where(x => x.Product != null && x.Product.ProductsID == product.ProductsID).ToList().FirstOrDefault().NumberOfProducts += 1;
            }
            else if (request.remove)
            {
                CartElement cartElement = cart.CartElements.Where(x => x.Product != null && x.Product.ProductsID == product.ProductsID).ToList().FirstOrDefault();
                if(cartElement != null && cartElement.NumberOfProducts > 0)
                {
                    cartElement.NumberOfProducts -= 1;
                }
            }
            _db.SaveChanges();
            return Json(new { });
        }

        [HttpPost]
        public IActionResult Search(IFormCollection collection)
        {
            Dictionary<String, List<String>> result = new Dictionary<string, List<string>>();
            foreach(String key in collection.Keys)
            {
                if (key.Contains("add-filter-select-"))
                {
                    String number = key.Split("-")[3];
                    String value = collection["choose-" + number];
                    List<String> found;
                    if(!result.TryGetValue(collection[key], out found))
                    {
                        result.Add(collection[key], new List<string>());
                    }
                    result[collection[key]].Add(value);
                }
            }
            Products[] products = (from p in _db.Products.Include("Distributors") select p).ToArray<Products>();
            foreach(String key in result.Keys)
            {
                switch (key)
                {
                    case "name":
                        products = Array.FindAll(products, product =>
                        {
                            foreach (String value in result[key])
                            {
                                if (product.ProductName.Contains(value))
                                {
                                    return true;
                                }
                            }
                            return false;
                        });
                        break;
                    case "desc":
                        products = Array.FindAll(products, product =>
                        {
                            foreach (String value in result[key])
                            {
                                if (product.Description.Contains(value) || product.ShortDescription.Contains(value))
                                {
                                    return true;
                                }
                            }
                            return false;
                        });
                        break;
                    case "distributor":
                        products = Array.FindAll(products, product =>
                        {
                            foreach (String value in result[key])
                            {
                                if(product.Distributors != null)
                                {
                                    foreach (Distributors distributor in product.Distributors)
                                    {
                                        if (distributor.CompanyName.Contains(value))
                                        {
                                            return true;
                                        }
                                    }
                                }
                                
                            }
                            return false;
                        });
                        break;
                    default:
                        break;
                }
            }
            SessionControl.setViewData(_db, ViewData, HttpContext);
            ViewData["products"] = products;
            return View("Index");
        }
    }
}