using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2_projekt
{
    public class Cart
    {
        public int CartID { get; set; }
        public ICollection<CartElement> CartElements { get; set; }
        public Users User { get; set; }
    }
}
