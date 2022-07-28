using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {   
        

        public List<User> UserList = new List<User>();
        public String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
        public string getRoleName(int id)
        {
            string role = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    String sql = "SELECT * FROM UserRoles WHERE Id = " + id;
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                            while (reader.Read())
                            {
                                role = reader["userRole"].ToString();

                          
                            }
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());

            }
            return role;
        }

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
                    String sql = "SELECT FK_Role_Id  FROM Users WHERE  id = '" + userId+"'";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                roleId = reader["FK_Role_Id"].ToString();
                            }
                        }
                    }
                    connection.Close();
                }
                if (Int32.Parse(roleId) == 2)
                {
                    
 
                    Response.Redirect("/");
                }
            }


            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    String sql = "SELECT * FROM Users";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                            while (reader.Read())
                            {

                                User user = new User();
                                user.email =  reader["email"].ToString();
                                user.id = reader["id"].ToString();
                                user.role = getRoleName(Int32.Parse(reader["Fk_Role_Id"].ToString()));

                                UserList.Add(user);
                            }
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());

            }
        }
    }
    public class User
    {
        public String id;
        public String email;
        public String role;
    }
}
