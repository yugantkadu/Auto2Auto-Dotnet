using Razorpay.Api;
using Razorpay.Api.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Auto2Auto.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class RazorpayApiController : ApiController
    {
        RazorpayClient client = new RazorpayClient("rzp_test_6oS5DiV9W7efUx", "551J4zlKM4iy5kqKyYmGucyI"); //Please generate Your Key Id and Key Secret by from Settings → API Keys → Generate Key on Razorpay Dashboard

        [HttpGet]
        public IEnumerable<Order> FindAllOrders()
        {
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("count", 20);
            List<Order> order = client.Order.All(options);
            return order;
                //client.Order.All()
        }
        [HttpGet]
        public List<Order> FindOrderByRecieptId(String id)
        {
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("receipt", id);
            List<Order> order = client.Order.All(options);
            return order;
        }

        [HttpGet]
        public List<Payment> FindPaymentByOrderId(String id) {
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("order_id", id);
            List<Payment> payment = client.Payment.All(options);
            return payment;
        }
    }
}
