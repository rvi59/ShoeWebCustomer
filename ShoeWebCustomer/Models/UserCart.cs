using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeWebCustomer.Models
{
    public class UserCart
    {
        public int ProdId { get; set; }
        public int Cartid { get; set; }
        public int user_Id { get; set; }
        public int Quantity { get; set; }
        public string Prod_ShortName { get; set; }
        public float Prod_Selling { get; set; }
        public string Prod_Image_Path { get; set; }
        public float TotalPrice { get; set; }
    }
}