using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeWebCustomer.Models
{
    public class Size
    {     
        public int Size_Id { get; set; }    
        public int Size_Number { get; set; }
        public DateTime Size_CreatedDate { get; set; }
        public ICollection<Products> tblProducts { get; set; }
    }
}