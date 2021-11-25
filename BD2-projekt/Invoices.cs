using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BD2_projekt
{
    public class Invoices{
        public int InvoicesID { get; set; }
        public Customers Customer { get; set; }
        public DateTime InvoiceDate { get; set; }
        [Required]
        public ICollection<OrderUnit> OrderUnit { get; set; }
    }
}
