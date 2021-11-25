using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BD2_projekt{
    public class Users{
        public int UsersID { get; set; }
        [StringLength(50)]
        public String City { get; set; }
        [StringLength(100)]
        public String Street { get; set; }
        [StringLength(15)]
        public String Number { get; set; }
        [StringLength(6)]
        public String PostCode { get; set; }
        [StringLength(100)]
        public String Country { get; set; }
        [StringLength(100)]
        public String Email { get; set; }
        [StringLength(15)]
        public String Phone { get; set; }
        [StringLength(10)]
        public String NIP { get; set; }
        [MaxLength(500)]
        public byte[] Password { get; set; }
    }
}
