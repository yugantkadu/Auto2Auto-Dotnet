using Auto2Auto.Models;
using Razorpay.Api;
using Razorpay.Api.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auto2Auto.Controllers
{
    public class RazorpayController : Controller
    {
        RazorpayClient client = new RazorpayClient("rzp_test_6oS5DiV9W7efUx", "551J4zlKM4iy5kqKyYmGucyI");
        // GET: Razorpay
        public ActionResult Index(String orderId, String orderAmount, String manufacturerName, String brandImg, String retailerName, String email, String contact)
        {
            try
            {
                var Amount = orderAmount;
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", Amount);
                options.Add("receipt", orderId);
                options.Add("currency", "INR");
                options.Add("payment_capture", "1");
                Order order = client.Order.Create(options);
                ViewBag.orderid = Convert.ToString(order["id"]);
                ViewBag.Amount = Int32.Parse(orderAmount);
                ViewBag.manufacturerName = manufacturerName;
                ViewBag.brandImg = brandImg;
                ViewBag.retailerName = retailerName;
                ViewBag.email = email;
                ViewBag.contact = contact;
            }
            catch (Razorpay.Api.Errors.BadRequestError ex)
            {
            }
            catch (SignatureVerificationError ex)
            {
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        public ActionResult PaymentStatus(string razorpay_order_id, string razorpay_payment_id, string razorpay_signature)
        {
            bool IsValidRequest = true;
            bool IsPaymentSuccess = false;
            bool IsPaymentCapturedSuccess = false;

            try
            {
                if (!string.IsNullOrEmpty(razorpay_order_id) && !string.IsNullOrEmpty(razorpay_payment_id) && !string.IsNullOrEmpty(razorpay_signature))
                {

                    var payload = razorpay_order_id + '|' + razorpay_payment_id;

                    Utils.verifyWebhookSignature(payload, razorpay_signature, "551J4zlKM4iy5kqKyYmGucyI");

                    //You can write your business logic here
                    IsPaymentSuccess = true;

                    var Amount = 3000;
                    Payment payment = client.Payment.Fetch(razorpay_payment_id);
                    Dictionary<string, object> options = new Dictionary<string, object>();
                    options.Add("amount", Amount);
                    options.Add("currency", "INR");
                    Payment paymentCaptured = payment.Capture(options);

                    var PaymentId = Convert.ToString(paymentCaptured["id"]);
                    if (!String.IsNullOrEmpty(PaymentId))
                    {
                        IsPaymentCapturedSuccess = true;
                    }
                }
                else
                {
                    IsValidRequest = false;
                }



            }
            catch (Razorpay.Api.Errors.BadRequestError ex)
            {

            }
            catch (SignatureVerificationError ex)
            {
                IsValidRequest = false;
            }
            catch (Exception ex)
            {
            }

            ViewBag.IsValidRequest = IsValidRequest;
            ViewBag.IsPaymentSuccess = IsPaymentSuccess;
            ViewBag.IsPaymentCapturedSuccess = IsPaymentCapturedSuccess;
            return View();

        }
    }
}