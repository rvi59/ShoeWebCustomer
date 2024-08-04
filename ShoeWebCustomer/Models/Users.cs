using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ShoeWebCustomer.Models
{
    public class Users
    {

        public int User_Id { get; set; }


        [Required(ErrorMessage = "UserName Is Required")]
        [DisplayName("User Name")]
        public string U_UserName { get; set; }


        [Required(ErrorMessage = "Password Is Required")]
        [DisplayName("Enter Password")]
        [RegularExpression("^(?=.*[a-zA-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{6,}$", ErrorMessage = "The password must be at least 6 characters long and contain alphanumeric characters with symbols.")]
        [DataType(DataType.Password)]
        public string U_Password { get; set; }


        [Required(ErrorMessage = "Confirm Password Is Required")]
        [Compare("U_Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DisplayName("Enter Confirm Password")]
        [RegularExpression("^(?=.*[a-zA-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{6,}$", ErrorMessage = "The password must be at least 6 characters long and contain alphanumeric characters with symbols.")]
        [DataType(DataType.Password)]
        public string U_ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Email Is Required")]
        [DisplayName("Enter Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Enter Email in Proper Format.")]
        public string U_Email { get; set; }


        [Required(ErrorMessage = "FirstName Is Required")]
        [DisplayName("Enter FirstName")]
        [MaxLength(50, ErrorMessage = "First Name must be less than 50 characters")]
        public string U_FirstName { get; set; }


        [DisplayName("LastName")]
        [MaxLength(50, ErrorMessage = "Last Name must be less than 50 characters")]
        public string U_LastName { get; set; }


        public bool UserType { get; set; }

    }
}