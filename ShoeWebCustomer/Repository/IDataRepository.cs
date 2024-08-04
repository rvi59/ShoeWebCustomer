using ShoeWebCustomer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeWebCustomer.Repository
{
    public interface IDataRepository
    {
        IEnumerable<Products> GetTop4Sports();
        IEnumerable<Products> GetTop4Casual();
        IEnumerable<Products> GetTop4Formal();
        bool checkEmailExist(string email);
        void UserAdd(Users user);
        LoginUser Login(LoginUser Luser);
        IEnumerable<Products> GetShoeListByCategory(int id);
        IEnumerable<Products> GetShoeBySearch(string searchString);
        IEnumerable<Products> GetProductDetailsbyId(int id);
        void AddProdInCart(int quantity, int prodId, int UserId);

        string CartItembyId(int cartId,int userID);

        IEnumerable<UserCart> GetUserCart(int userID);

        string ForgetPassword(string email);

        string RecoverPassword(string email);

        public string UpdatePassword(string Password, string codeid);

        public string UpdateCart(List<UserCartUpdate> userCartUpdates);

        public DataTable GetCheckoutDT(int user_id);

        public string InsertBill(int U_Id, string Address, string mobile, string payment_id, string order_id);

        public DataTable GetUserBill(int user_id);

        public DataTable GetOrdersList(int user_id);



    }
}
