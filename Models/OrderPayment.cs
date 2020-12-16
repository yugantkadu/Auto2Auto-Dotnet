using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auto2Auto.Models
{
    public class OrderPayment
    {
        public int orderid { get; set; }
        public int orderAmount { get; set; }
        public String manufacturerName { get; set; }
        public String brandImg { get; set; }
        public String retailerName { get; set; }
        public String email { get; set; }
        public int contact { get; set; }
    }
}