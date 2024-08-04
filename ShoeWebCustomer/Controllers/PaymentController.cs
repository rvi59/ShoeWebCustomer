using Razorpay.Api;
using ShoeWebCustomer.Repository;
using ShoeWebCustomer.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoeWebCustomer.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IDataRepository _dataRepository;

        private readonly RazorpayService _razorpayService;

        public PaymentController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
            _razorpayService = new RazorpayService();
        }

        // GET: Payment
        public ActionResult Checkout()
        {

            int my_UserId = Convert.ToInt32(Session["LoginId"]);
            if (my_UserId == 0)
            {
                return RedirectToAction("UserLogin", "Account");
            }
            else
            {
                DataTable dt = new DataTable();

                dt = _dataRepository.GetCheckoutDT(my_UserId);
                return View(dt);
            }
        }


        //public ActionResult InsertUserBIll(string amount)
        //{
        //    string redUrl = "";

        //    int U_Id = Convert.ToInt32(Session["LoginId"]);


        //    //string mess = _dataRepository.InsertBill(U_Id, Address, mobile);
        //    string mess = "";

        //    if (mess == "Y")
        //    {
        //        redUrl = Url.Action("UserBill", "Payment");

        //    }
        //    else
        //    {
        //        redUrl = Url.Action("error");
        //    }

        //    return Json(new { url = redUrl });
        //}




        [HttpPost]
        public ActionResult ConfirmPayment(string Address, string mobile, string amount)
        {
            if (string.IsNullOrWhiteSpace(amount) || !int.TryParse(amount, out int amountInRupees))
            {
                return Json(new { error = "Invalid amount" });
            }
            int amountInPaise = amountInRupees * 100; // Convert to paise

            var order = _razorpayService.CreateOrder(amountInPaise);

            int amount1 = Convert.ToInt32(order.Attributes["amount"]);
            string order_id1 = order.Attributes["id"];

            var response = new
            {
                key = System.Configuration.ConfigurationManager.AppSettings["razorpay_key"],
                amount = amount1,
                order_id = order_id1 //order.Attributes["order_id"]
            };

            return Json(response);
        }




        [HttpPost]
        public ActionResult InsertUserBill(string payment_id, string order_id, string Address, string mobile)
        {
            string redUrl = "";
            int U_Id = Convert.ToInt32(Session["LoginId"]);

            string mess = _dataRepository.InsertBill(U_Id, Address, mobile, payment_id,order_id);

                if (mess == "Y")
                {
                    redUrl = Url.Action("UserBill", "Payment");
            
                }
                else
                {
                    redUrl = Url.Action("error");
                }
            
                return Json(new { url = redUrl });
        }



        public ActionResult UserBIll()
        {
            int my_UserId = Convert.ToInt32(Session["LoginId"]);

            if (my_UserId == 0)
            {
                return RedirectToAction("UserLogin", "Account");
            }
            else
            {
                DataTable dt = new DataTable();
                dt = _dataRepository.GetUserBill(my_UserId);
                return View(dt);
            }
        }


        public ActionResult MyOrders()
        {
            int my_UserId = Convert.ToInt32(Session["LoginId"]);

            if (my_UserId == 0)
            {
                return RedirectToAction("UserLogin", "Account");
            }
            else
            {
                DataTable dt = new DataTable();
                dt = _dataRepository.GetOrdersList(my_UserId);

                return View(dt);
            }

        }

      

    }
}