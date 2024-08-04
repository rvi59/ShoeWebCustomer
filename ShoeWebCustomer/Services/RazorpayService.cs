using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeWebCustomer.Services
{

    public class RazorpayService
    {
        private readonly string _key;
        private readonly string _secret;

        public RazorpayService()
        {
            _key = System.Configuration.ConfigurationManager.AppSettings["razorpay_key"];
            _secret = System.Configuration.ConfigurationManager.AppSettings["razorpay_secret"];
        }


        private RazorpayClient GetClient()
        {
            return new RazorpayClient(_key, _secret);
        }

        public Order CreateOrder(int amount)
        {
            var client = GetClient();

            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", amount); // amount in paise
            options.Add("currency", "INR");
            options.Add("receipt", Guid.NewGuid().ToString());

            try
            {
                Order order = client.Order.Create(options);
                if (order.Attributes["amount"] == null)
                {
                    throw new ApplicationException("Amount is null in the order response");
                }
                return order;
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately
                throw new ApplicationException("Error creating Razorpay order", ex);
            }
        }




       
    }

}