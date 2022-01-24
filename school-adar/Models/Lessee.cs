using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace school_adar.Models
{
    public class Lessee
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public byte[] Image { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}