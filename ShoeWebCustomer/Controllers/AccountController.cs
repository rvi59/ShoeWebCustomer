using ShoeWebCustomer.Models;
using ShoeWebCustomer.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ShoeWebCustomer.Controllers
{
    public class AccountController : Controller
    {

        private readonly IDataRepository _dataRepository;

        public AccountController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        // GET: Account
        public ActionResult UserRegistration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserRegistration(Users users)
        {

            if (ModelState.IsValid)
            {
                bool chkEMail = _dataRepository.checkEmailExist(users.U_Email);

                if (chkEMail==true)
                {
                    ViewBag.ExistMessage = "Email Id Already Exists.";
                    return View();
                }
                else
                {
                    _dataRepository.UserAdd(users);
                    return RedirectToAction("UserLogin", "Account");
                }

            }
            return View(users);
        }

        public ActionResult UserLogin()
        {
            return View();
        }


        [HttpPost]
        public ActionResult UserLogin(LoginUser user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var LoginUser = _dataRepository.Login(user);

                    if (LoginUser != null)
                    {
                        Session["LoginUser"] = LoginUser.U_UserName;
                        Session["LoginId"] = LoginUser.User_Id;
                        string a = Session["LoginUser"].ToString();


                        if (LoginUser.UserType == true)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return Redirect("http://onlineshooeadmin.somee.com");
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invalid username or password.";
                        //return RedirectToAction("UserLogin", "Account");
                        return View();
                    }

                }
                catch (SqlException ex)
                {
                    return View("Error");
                }
            }
            return View(user);
        }


        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("UserLogin", "Account");
        }


        public ActionResult ForgetPass(string email)
        {

          string myPassMessage =   _dataRepository.ForgetPassword(email);
            
          return Json(myPassMessage, JsonRequestBehavior.AllowGet);
               
        }


        public ActionResult RecoverPassword(string id)
        {
            string errorMessage = _dataRepository.RecoverPassword(id);

            if (string.IsNullOrEmpty(errorMessage))
            {
                TempData["recovermessage"] = errorMessage;
            }
            else
            {
                TempData["recovermessage"] = errorMessage;
            }
            
            return View();
        }

        
        public ActionResult ResetPassword(string Password, string codeid)
        {

            string Insertmessage = _dataRepository.UpdatePassword(Password, codeid);

            if (!string.IsNullOrEmpty(Insertmessage))
            {
                // Password updated successfully
                return Json(new { success = true, message = "Password updated successfully", url = Url.Action("UserLogin", "Account") });
            }
            else
            {
                // Password update failed
                return Json(new { success = false, message = "Password update failed" });
            }

        }


    }

}