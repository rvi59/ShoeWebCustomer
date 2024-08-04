using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ShoeWebCustomer.Models
{
    public class LoginUser
    {
        public int User_Id { get; set; }
        public string U_UserName { get; set; }
        public bool UserType { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        [DisplayName("Enter Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Enter Email in Proper Format.")]
        public string U_Email { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [DisplayName("Enter Password")]
        [DataType(DataType.Password)]
        public string U_Password { get; set; }

        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }

    }
}