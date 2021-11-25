using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2_projekt
{
    public class OrderUnit
    {
        public int OrderUnitID { get; set; }
        public Products Product { get; set; }
        public int NumberOfProducts { get; set; }
        public double unitPrice { get; set; }
    }
}
