using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2_projekt
{
    public class JSONAddRemoveToCart
    {
        [BindProperty]
        public bool add { get; set; }
        [BindProperty]
        public bool remove { get; set; }
        [BindProperty]
        public int ID { get; set; }
    }
}
