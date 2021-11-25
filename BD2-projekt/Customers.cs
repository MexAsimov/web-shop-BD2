using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BD2_projekt{
    [Table("Customers")]
    public class Customers: Users{
        public int CustomersID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String CardNumber { get; set; }
    }
}
