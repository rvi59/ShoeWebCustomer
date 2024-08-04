
using ShoeWebCustomer.Models;
using ShoeWebCustomer.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoeWebCustomer.Controllers
{
    public class ProductController : Controller
    {
        private readonly IDataRepository _dataRepository;

        public ProductController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public ActionResult ProductListbyCategory(int id)
        {
            if (id > 0)
            {
                TempData["ProdCategotydata"] = _dataRepository.GetShoeListByCategory(id);
            }

            return View();
        }


        public ActionResult SearchProduct(string searchString)
        {

            if (searchString != null)
            {

                TempData["ProdSearchdata"] = _dataRepository.GetShoeBySearch(searchString);

            }

            return View();
        }

       
        public ActionResult ProductDetails(int id)
        {
            if (id > 0)
            {
                TempData["ProdDetaildata"] = _dataRepository.GetProductDetailsbyId(id);
            }
            return View();
        }

        [HttpPost]
        public JsonResult InsertInCart(int quantity, int prodId)
        {
            string cartDetailsUrl = "";
            int userID = Convert.ToInt32(Session["LoginId"]);

            if (userID == 0)
            {
                 cartDetailsUrl = Url.Action("UserLogin", "Account");
            }
            else
            {
                _dataRepository.AddProdInCart(quantity, prodId, userID);

                cartDetailsUrl = Url.Action("CartDetails", "Product");
            }
           
            return Json(new { url = cartDetailsUrl });
        }


       // [HttpPost]
        public JsonResult DeleteCartItem(int cartId)
        {
            string cartmess = "";
            int userID = Convert.ToInt32(Session["LoginId"]);

            if (cartId != 0 )
            {
                cartmess = _dataRepository.CartItembyId(cartId, userID);
            }

            return Json(new { success = !string.IsNullOrEmpty(cartmess), message = cartmess });
        }


        public ActionResult CartDetails()
        {
            int userID = Convert.ToInt32(Session["LoginId"]);
            var data = new List<UserCart>();
            List<UserCart> newData = new List<UserCart>();

            if (userID > 0)
            {
                data = _dataRepository.GetUserCart(userID).ToList();

                foreach (var item in data)
                {
                    var mycartDetails = new UserCart
                    {
                        ProdId = item.ProdId,
                        Cartid = item.Cartid,
                        Quantity = item.Quantity,
                        Prod_ShortName = item.Prod_ShortName,
                        Prod_Selling = item.Prod_Selling,
                        Prod_Image_Path = item.Prod_Image_Path,
                        TotalPrice = item.Quantity * item.Prod_Selling
                    };

                    newData.Add(mycartDetails);
                }

            }
            else
            {
                return RedirectToAction("UserLogin", "Account");
            }

            return View(newData);
        }



        public ActionResult InsertCheckout(List<UserCartUpdate> userCartUpdates)
        {
            string checkoutUrl = "";
            string my_Id = Session["LoginId"].ToString();

            foreach (UserCartUpdate uc in userCartUpdates)
            {
                uc.user_Id = Convert.ToInt32(my_Id);
            }
            string mymess = _dataRepository.UpdateCart(userCartUpdates);

            if (mymess == "1")
            {
                checkoutUrl = Url.Action("Checkout", "Payment");
            }
            return Json(new { url = checkoutUrl });
        }

       

    }
}