using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages
{
    public class HomeModel : PageModel
    {
        public bool _isAdminUser = false;
        private string _role;
        private string errorMessage;
        public void OnGet()
        {
            string userId = Request.Cookies["UserID"];
            if (userId == null)
            {
                Response.Redirect("/");
            }
            else
            {
                try
                {

                    String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {

                        connection.Open();
                        String sql = "SELECT * FROM Users WHERE id = '" + userId + "';";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {

                            using (SqlDataReader reader = command.ExecuteReader())
                            {

                                if (reader.Read())
                                {

                                    _role = reader["FK_Role_Id"].ToString();

                                }


                            }

                        }

                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                }
                if (_role == "1")
                {
                    _isAdminUser = true;
                }

                return;

            }
        }
       
    }
}
