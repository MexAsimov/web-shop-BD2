using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BD2_projekt
{
    public class SessionControl{
        public static void setViewData(Context _db, ViewDataDictionary ViewData, HttpContext httpContext)
        {
            String name = httpContext.Session.GetString("user");
            Distributors dis = (from dist in _db.Distributors where dist.Email == name select dist).FirstOrDefault<Distributors>();
            ViewData["session"] = name;
            ViewData["userType"] = httpContext.Session.GetString("userType");
        }

        public static void SetUserType(HttpContext httpContext)
        {
            Context _db = new Context();
            String name = httpContext.Session.GetString("user");
            Distributors dis = (from dist in _db.Distributors where dist.Email == name select dist).FirstOrDefault<Distributors>();
            if (dis == null)
            {
                httpContext.Session.SetString("userType", "Customer");
            }
            else
            {
                httpContext.Session.SetString("userType", "Distributor");
            }
        }
    }
}
