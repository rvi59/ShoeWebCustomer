using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeWebCustomer.Models
{
    public class Brands
    {
        
        public int Brand_Id { get; set; }  
        public string Brand_Name { get; set; } 
        public DateTime Brand_CreatedDate { get; set; }
        public ICollection<Products> tblProducts { get; set; }
    }
}