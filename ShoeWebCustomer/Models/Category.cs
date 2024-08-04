using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeWebCustomer.Models
{
    public class Category
    {
        public int Category_Id { get; set; }       
        public string Category_Name { get; set; }
        public DateTime Category_CreatedDate { get; set; }
    }
}