using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Text;

namespace Automation_Test_Data_App.Pages.Admin.Users
{
    public class AddUserModel : PageModel
    {
        public String errorMessage = "";
        public String successMessage = "";
        public String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";


        public void OnGet()
        {
            string userId = Request.Cookies["UserID"];
            if (userId == null)
            {
                Response.Redirect("/");
            }
            else
            {
                String roleId = "0";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT *  FROM Users WHERE  id = '" + userId+ "'";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                roleId = reader["Fk_Role_Id"].ToString();
                            }
                        }
                    }
                }
                if (Int32.Parse(roleId) == 1)
                {
                    return;
                }
                else
                {
                    Response.Redirect("/");
                }
            }

        }
        public void OnPost()
        {
            string email  =  Request.Form["email"];
            string password = Request.Form["password"];
            string pwconfirm = Request.Form["pwconfirm"];
            string role = Request.Form["role"];

            //Create unique ID
            StringBuilder builder = new StringBuilder();
            Enumerable
               .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(11)
                .ToList().ForEach(e => builder.Append(e));
            string id = builder.ToString();

            if (email.Length == 0 || password.Length == 0 || pwconfirm.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;

            }
            if (!password.Equals(pwconfirm))
            {
                errorMessage = "Passwords does not match.";
                return;
            }

            try
            {

               
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Users" +
                                "(id,Email, PasswordHash , Fk_Role_Id) VALUES" +
                                "(@id,@Email, @Password, @roleId);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@roleId", Int32.Parse(role));
                        command.ExecuteNonQuery();

                    }
                    successMessage = "User Added Successfully";
                    connection.Close();
                    return;

                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }


        }
    }
}
