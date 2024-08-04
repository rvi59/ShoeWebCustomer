using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeWebCustomer.Models
{
    public class Products
    {
       
        public int Prod_Id { get; set; }       
        public string Prod_Name { get; set; }
        public string Prod_ShortName { get; set; }     
        public float Prod_Price { get; set; }      
        public float Prod_Selling { get; set; }       
        public string Prod_Description { get; set; }     
        public string Prod_Image_Path { get; set; }      
        public int Quantity { get; set; }
        public int BrandId { get; set; }      
        public Brands Brand { get; set; }
        public int CategoryId { get; set; }
        
        public Category Category { get; set; }
        public int SizeId { get; set; }      
        public Size Size { get; set; }
        public DateTime Prod_CreatedDate { get; set; }
    }
}