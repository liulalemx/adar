using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace school_adar.Models
{
    public class Housing
    {
        public int ID { get; set; }
        public int LessorID { get; set; }
        public string Location { get; set; }
        public double Size { get; set; }
        public double Price { get; set; }
        public byte[] Image { get; set; }
        public string Features { get; set; }
        public string Condition { get; set; }

        //[Required]
        public virtual Lessor Lessor { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}