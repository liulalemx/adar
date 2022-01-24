using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace school_adar.Models
{
    public class Review
    {
        public int ID { get; set; }
        public int LesseeID { get; set; }
        public int HousingID { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }

        public virtual Housing Housing { get; set; }
        public virtual Lessee Lessee { get; set; }
    }
}