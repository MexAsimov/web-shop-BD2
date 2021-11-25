using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BD2_projekt
{
    public class CartElement
    {
        public int CartElementID { get; set; }
        public Products Product { get; set; }
        public int NumberOfProducts { get; set; }
    }
}
