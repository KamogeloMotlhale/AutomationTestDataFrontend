using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.Admin.Users
{
    public class EditModel : PageModel
    {
        public User UserInfo = new User();
        public String errorMessage = "";
        public String successMessage = "";
        String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";


        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {

                String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    String sql = "SELECT * FROM User WHERE id= '"+id+"'";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {

                                UserInfo.id =  reader["Id"].ToString();
                                UserInfo.email = reader["email"].ToString();
                                UserInfo.role = reader["role"].ToString();

                            }


                        }

                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

        }


        public void OnPost()
        {
            UserInfo.id = Request.Form["id"];
            UserInfo.email = Request.Form["email"];
            UserInfo.role = Request.Form["role"];
           

            if (UserInfo.email.Length == 0 || UserInfo.role.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;

            }
            //save new Life assured to DB
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE  User" +
                                 "SET email=@email, role=@role" +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@email", UserInfo.email);
                        command.Parameters.AddWithValue("@role", UserInfo.role);




                        command.ExecuteNonQuery();

                    }
                    UserInfo.email = ""; 
                    UserInfo.role = "";
                    
                    successMessage = "New Life Assured Updated Successfully";

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
