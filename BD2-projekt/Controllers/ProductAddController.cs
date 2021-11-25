using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BD2_projekt.Controllers
{
    public class ProductAddController : Controller
    {
        Context _db = new Context();
        public IActionResult Index()
        {
            StoragePlaces[] places = (from sp in _db.StoragePlaces select sp).ToArray();
            ViewData["places"] = places;
            String userName = HttpContext.Session.GetString("user");
            Distributors dis = (from dist in _db.Distributors where dist.Email == userName select dist).FirstOrDefault<Distributors>();

            if(userName == "" || dis == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            //ViewData["session"] = HttpContext.Session.GetString("user");
            //ViewData["test"] = dis;
            SessionControl.setViewData(_db, ViewData, HttpContext);
            return View("Views/AddProduct/Index.cshtml");
        }
        public IActionResult AddProduct(IFormCollection collection)
        {
            StoragePlaces[] places = (from sp in _db.StoragePlaces select sp).ToArray();
            String userName = HttpContext.Session.GetString("user");
            Distributors dis = (from dist in _db.Distributors where dist.Email == userName select dist).FirstOrDefault<Distributors>();
            Products product = new Products();
            product.ProductName = collection["name"];
            product.ShortDescription = collection["shrt-desc"];
            product.Description = collection["desc"];
            product.MeasureUnit = collection["unit"];
            product.Quantity = Int32.Parse(collection["quantity"]);
            product.price = Double.Parse(collection["price"]);
            product.StoragePlace = Array.Find(places, place => place.StoragePlacesID == Int32.Parse(collection["place"]));
            if(dis.DistributedProducts == null)
            {
                ICollection < Products > products = new List<Products>();
                dis.DistributedProducts = products;
            }
            dis.DistributedProducts.Add(product);
            product.Distributors = new List<Distributors>();
            product.Distributors.Add(dis);
            _db.Products.Add(product);
            _db.SaveChanges();
            //ViewData["session"] = HttpContext.Session.GetString("user");
            SessionControl.setViewData(_db, ViewData, HttpContext);
            return View("Views/Home/Index.cshtml");
        }

    
    }
}