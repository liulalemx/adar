using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace school_adar.Models
{
    public class Lessor
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Housing> Housings { get; set; }
    }
}