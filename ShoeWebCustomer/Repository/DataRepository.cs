using ShoeWebCustomer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Drawing;
using System.Web.Helpers;
using Newtonsoft.Json;
using System.Web.UI.WebControls;

namespace ShoeWebCustomer.Repository
{
    public class DataRepository : IDataRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MYCS"].ConnectionString;

        public IEnumerable<Products> GetTop4Sports()
        {
            var data = new List<Products>();
            var dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    SqlCommand cmd = new SqlCommand("sp_GetTop4Sports", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    con.Open();
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        var dataItem = new Products();
                        dataItem.Prod_Id = Convert.ToInt32(row["Prod_Id"]);
                        dataItem.Prod_ShortName = row["Prod_ShortName"].ToString();
                        dataItem.Prod_Selling = float.Parse(row["Prod_Selling"].ToString());
                        dataItem.Prod_Price = float.Parse(row["Prod_Price"].ToString());
                        dataItem.Prod_Image_Path = row["Prod_Image_Path"].ToString();
                        dataItem.Brand = new Brands { Brand_Name = row["Brand_Name"].ToString() };

                        data.Add(dataItem);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return data;

        }


        public IEnumerable<Products> GetTop4Casual()
        {
            var data = new List<Products>();
            var dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    SqlCommand cmd = new SqlCommand("sp_GetTop4Casual", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    con.Open();
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        var dataItem = new Products();
                        dataItem.Prod_Id = Convert.ToInt32(row["Prod_Id"]);
                        dataItem.Prod_ShortName = row["Prod_ShortName"].ToString();
                        dataItem.Prod_Selling = float.Parse(row["Prod_Selling"].ToString());
                        dataItem.Prod_Price = float.Parse(row["Prod_Price"].ToString());
                        dataItem.Prod_Image_Path = row["Prod_Image_Path"].ToString();
                        dataItem.Brand = new Brands { Brand_Name = row["Brand_Name"].ToString() };

                        data.Add(dataItem);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return data;

        }


        public IEnumerable<Products> GetTop4Formal()
        {
            var data = new List<Products>();
            var dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    SqlCommand cmd = new SqlCommand("sp_GetTop4Formal", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    con.Open();
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        var dataItem = new Products();
                        dataItem.Prod_Id = Convert.ToInt32(row["Prod_Id"]);
                        dataItem.Prod_ShortName = row["Prod_ShortName"].ToString();
                        dataItem.Prod_Selling = float.Parse(row["Prod_Selling"].ToString());
                        dataItem.Prod_Price = float.Parse(row["Prod_Price"].ToString());
                        dataItem.Prod_Image_Path = row["Prod_Image_Path"].ToString();
                        dataItem.Brand = new Brands { Brand_Name = row["Brand_Name"].ToString() };

                        data.Add(dataItem);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return data;

        }

        public void UserAdd(Users user)
        {
            DateTime createdDate = DateTime.Now;
            string encodedPassword = PasswordHelper.EncodePassword(user.U_Password);
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_Registration", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", user.U_UserName.ToString());
                    cmd.Parameters.AddWithValue("@Password", encodedPassword);
                    cmd.Parameters.AddWithValue("@Email", user.U_Email.ToString());
                    cmd.Parameters.AddWithValue("@FirstName", user.U_FirstName.ToString());
                    cmd.Parameters.AddWithValue("@CreatedDate", createdDate);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }


        bool IDataRepository.checkEmailExist(string email)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spIsEmailExists", con);
                    cmd.CommandType = CommandType.StoredProcedure;                   
                    cmd.Parameters.AddWithValue("@user_Email", email);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }


        public LoginUser Login(LoginUser user)
        {

            string encodedPassword = PasswordHelper.EncodePassword(user.U_Password);
          
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    SqlCommand cmd = new SqlCommand("sp_Login", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", user.U_Email);
                    cmd.Parameters.AddWithValue("@Password", encodedPassword);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        return new LoginUser
                        {
                            User_Id = Convert.ToInt32(dr["User_Id"]),
                            U_UserName = dr["U_UserName"].ToString(),
                            U_Password = dr["U_Password"].ToString(),
                            UserType = Convert.ToBoolean(dr["UserType"])
                        };
                    }

                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return null;
        }

        public IEnumerable<Products> GetShoeListByCategory(int id)
        {

            var data = new List<Products>();
            var dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    SqlCommand cmd = new SqlCommand("sp_GetshoeByCategory", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CategoryId", id);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    con.Open();
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        var dataItem = new Products();
                        dataItem.Prod_Id = Convert.ToInt32(row["Prod_Id"]);
                        dataItem.Prod_ShortName = row["Prod_ShortName"].ToString();
                        dataItem.Prod_Selling = float.Parse(row["Prod_Selling"].ToString());
                        dataItem.Prod_Price = float.Parse(row["Prod_Price"].ToString());
                        dataItem.Prod_Image_Path = row["Prod_Image_Path"].ToString();
                        dataItem.Brand = new Brands { Brand_Name = row["Brand_Name"].ToString() };
                        dataItem.Category = new Category { Category_Name = row["Category_Name"].ToString() };

                        data.Add(dataItem);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return data;

        }

        public IEnumerable<Products> GetShoeBySearch(string searchString)
        {

            var data = new List<Products>();
            var dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    SqlCommand cmd = new SqlCommand("sp_SearchProduct", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Searchtxt", searchString);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    con.Open();
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        var dataItem = new Products();
                        dataItem.Prod_Id = Convert.ToInt32(row["Prod_Id"]);
                        dataItem.Prod_ShortName = row["Prod_ShortName"].ToString();
                        dataItem.Prod_Selling = float.Parse(row["Prod_Selling"].ToString());
                        dataItem.Prod_Price = float.Parse(row["Prod_Price"].ToString());
                        dataItem.Prod_Image_Path = row["Prod_Image_Path"].ToString();
                        dataItem.Brand = new Brands { Brand_Name = row["Brand_Name"].ToString() };


                        data.Add(dataItem);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return data;

        }

        public IEnumerable<Products> GetProductDetailsbyId(int id)
        {

            var data = new List<Products>();
            var dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    SqlCommand cmd = new SqlCommand("sp_GetProductDetailsbyId", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductId", id);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    con.Open();
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        var dataItem = new Products();
                        dataItem.Prod_Id = Convert.ToInt32(row["Prod_Id"]);
                        dataItem.Prod_ShortName = row["Prod_ShortName"].ToString();
                        dataItem.Prod_Selling = float.Parse(row["Prod_Selling"].ToString());
                        dataItem.Prod_Price = float.Parse(row["Prod_Price"].ToString());
                        dataItem.Prod_Description = row["Prod_Description"].ToString();
                        dataItem.Prod_Image_Path = row["Prod_Image_Path"].ToString();
                        dataItem.Brand = new Brands { Brand_Name = row["Brand_Name"].ToString() };
                        dataItem.Category = new Category { Category_Name = row["Category_Name"].ToString() };
                        dataItem.Size = new Models.Size { Size_Number = Convert.ToInt32(row["Size_Number"]) };

                        data.Add(dataItem);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return data;

        }

        public void AddProdInCart(int quantity, int prodId, int UserId)
        {

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spInsertProdInCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@prodId", prodId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    con.Open();
                    cmd.ExecuteNonQuery();

                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }


        public string CartItembyId(int cartId, int userID)
        {
            string mess = "";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    if (cartId != 0)
                    {
                        SqlCommand cmd = new SqlCommand("sp_DelCartItem", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Cart_Id", cartId);
                        cmd.Parameters.AddWithValue("@User_Id", userID);
                        con.Open();

                        int a = cmd.ExecuteNonQuery();

                        if (a>0)
                        {
                             mess = "Deleted";
                        }
                        else
                        {
                             mess = "Failed";
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return mess;
        }

        public IEnumerable<UserCart> GetUserCart(int id)
        {

            var data = new List<UserCart>();
            var dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    SqlCommand cmd = new SqlCommand("sp_UsersCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@user_Id", id);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    con.Open();
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        var dataItem = new UserCart();
                        dataItem.ProdId = Convert.ToInt32(row["ProdId"]);
                        dataItem.Cartid = Convert.ToInt32(row["Cartid"]);
                        dataItem.Quantity = Convert.ToInt32(row["Quantity"]);
                        dataItem.Prod_ShortName = row["Prod_ShortName"].ToString();
                        dataItem.Prod_Selling = float.Parse(row["Prod_Selling"].ToString());
                        dataItem.Prod_Image_Path = row["Prod_Image_Path"].ToString();
                        dataItem.TotalPrice = float.Parse(row["TotalPrice"].ToString());
                        data.Add(dataItem);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return data;

        }


        public string ForgetPassword(string email)
        {
            string mess = "";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spIsEmailValid", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@user_Email", email);
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        string myGuid = Guid.NewGuid().ToString();
                        int uid = Convert.ToInt32(dt.Rows[0][0]);
                        DateTime myDate = DateTime.Now;
                        SqlCommand cmd2 = new SqlCommand("spForgetPass", con);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("@Pass_Id", myGuid);
                        cmd2.Parameters.AddWithValue("@user_Id", uid);
                        cmd2.Parameters.AddWithValue("@RequestDateTime", myDate);

                        int a = cmd2.ExecuteNonQuery();

                        if (a > 0)
                        {
                            string ToEmailAddress = dt.Rows[0][3].ToString();
                            string Username = dt.Rows[0][1].ToString();

                            string EmailBody = "Hi , " + Username + ",<br/><br/> Copy the link below to reset your Password <br/> <br/> http://onlineshooe.somee.com/Account/RecoverPassword?id=" + myGuid;

                            MailMessage PasRecMail = new MailMessage("salman123456828@gmail.com", ToEmailAddress);
                            PasRecMail.Body = EmailBody;
                            PasRecMail.IsBodyHtml = true;
                            PasRecMail.Subject = "Reset Password";

                            using (SmtpClient smtp = new SmtpClient())
                            {
                                smtp.UseDefaultCredentials = false;
                                smtp.Credentials = new NetworkCredential("salman123456828@gmail.com", "zyoblegrlqvnreoh");
                                smtp.EnableSsl = true;

                                smtp.Host = "smtp.gmail.com";
                                smtp.Port = 587;
                                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                                smtp.Send(PasRecMail);
                                return mess = "Recovery Password Link has been send to your Registered email Id.";
                            }
                        }

                    }
                    else
                    {
                        return mess = "Email Is Invalid";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return mess;
        }


        public string RecoverPassword(string id)
        {
            string Errormess = "";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    string QString = id;

                    if (QString != null)
                    {

                        SqlCommand cmd = new SqlCommand("spRecoverPass", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Pass_Id", id);
                        con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);


                        if (dt.Rows.Count > 0)
                        {
                            //string a = dt.Rows[0]["Pass_Id"].ToString();
                            //string b = dt.Rows[0]["user_Id"].ToString();

                            Errormess = dt.Rows[0]["user_Id"].ToString();
                        }
                        else
                        {
                            Errormess = "Your Password link has expired.. try again.";
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return Errormess;
        }

        public string UpdatePassword(string Password, string codeid)
        {

            string Mess = "";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spUpdatePass", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@user_Pass", Password);
                    cmd.Parameters.AddWithValue("@user_Id", codeid);
                    con.Open();
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        Mess = "Password Updated Successfully";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return Mess;
        }


        public string UpdateCart(List<UserCartUpdate> userCartUpdates)
        {
            string msg = "";

            DataTable mytable = new DataTable();
            mytable.Columns.Add("Cartid", typeof(int));
            mytable.Columns.Add("Quantity", typeof(int));
            mytable.Columns.Add("user_Id", typeof(int));

            foreach (var update in userCartUpdates)
            {
                mytable.Rows.Add(update.Cartid, update.Quantity, update.user_Id);
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spUpdateCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter parameter = cmd.Parameters.AddWithValue("@UpdateType", mytable);
                    parameter.SqlDbType = SqlDbType.Structured;

                    con.Open();
                   
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        msg = "1";
                    }
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }

            return msg;
        }


        public DataTable GetCheckoutDT(int user_id)
        {
            DataTable dataTable = new DataTable();

            try
            {
                if (user_id>0)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("sp_Checkout", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@user_Id", user_id);
                        con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            return dt;
                        }
                       
                    }
                }
               

            }
            catch (Exception)
            {

                throw;
            }
            return dataTable;
        }


        public string InsertBill(int U_Id, string Address, string mobile, string payment_id, string order_id)
        {
            string amess = "";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertOrder", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@user_Id", U_Id);
                    cmd.Parameters.AddWithValue("@Address", Address);
                    cmd.Parameters.AddWithValue("@Mobile", mobile);
                    cmd.Parameters.AddWithValue("@OrderId", order_id);
                    cmd.Parameters.AddWithValue("@PaymentId", payment_id);
                    con.Open();
                    int a = cmd.ExecuteNonQuery();
                    if (a == 0)
                    {
                        amess = "N";
                    }
                    else
                    {
                        amess = "Y";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return amess;
        }


        public DataTable GetUserBill(int user_id) {

            DataTable dataTable = new DataTable();

            try
            {
                if (user_id > 0)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("sp_GetUserBill", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@user_Id", user_id);
                        con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            return dt;
                        }

                    }
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            return dataTable;

        }

        public DataTable GetOrdersList(int user_id)
        {
            DataTable dataTable = new DataTable();

            try
            {
                if (user_id > 0)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("sp_OrderList", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@user_Id", user_id);
                        con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            return dt;
                        }

                    }
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            return dataTable;

        }

       
    }
}