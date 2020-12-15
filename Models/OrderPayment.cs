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
        public int manufacturerName { get; set; }
        public int brandImg { get; set; }
        public int retailerName { get; set; }
        public int email { get; set; }
        public int contact { get; set; }
    }
}