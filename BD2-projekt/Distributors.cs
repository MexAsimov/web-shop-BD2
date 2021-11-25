using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BD2_projekt{
    [Table("Distributors")]
    public class Distributors: Users{
        public int DistributorsID { get; set; }
        public String CompanyName { get; set; }
        public String BankAccountNumber { get; set; }
        public virtual ICollection<Products> DistributedProducts { get; set; }
    }
}
