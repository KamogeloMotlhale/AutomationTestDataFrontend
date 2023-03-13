using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Automation_Test_Data_App.Pages.Shared;

namespace Automation_Test_Data_App.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public String errorMessage = "";
        public String successMessage = "";

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void SetUserID(string value)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(60);
            Response.Cookies.Append("UserID", value, option);
        }

        public void isLoggedIn()
        {
            string id = Request.Cookies["UserID"];
            if (id == null)
            {
                Response.Redirect("/");
            }
            else
            {
                return;
            }
        }

    

        public string GetUserID()
        {
            string userId = Request.Cookies["UserID"];
            return userId;
        }

        public void OnPost()
        {
            string email = Request.Form["email"];
            string password = Request.Form["password"];

            
            if (email.Length == 0 || password.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;

            }
            try
            {
                String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
                var dbUsId = "";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT 1 FROM Users WHERE Email = '" + email + "'";
                    int recCount = 0;

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        while (reader.Read())
                        {
                                recCount++;
                        }
                    }

                    if(recCount > 0)
                    {
                        sql = "SELECT Id, PasswordHash FROM Users WHERE Email = '" + email+"'";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            while (reader.Read())
                            {
                                    if (Password.DecodeFrom64((reader["PasswordHash"].ToString())) .Equals(password))
                                    {
                                        dbUsId = reader["Id"].ToString();
                                        SetUserID(dbUsId);




                                    }
                                    else
                                    {
                                        errorMessage = "Incorrect Password";
                                    }
                            }
                        }
                        connection.Close();
                        if (dbUsId == "")
                        {
                            return;

                        }
                        else
                        {
                            Response.Redirect("home");
                        }
                    }
                    else
                    {
                        errorMessage = "No user with the email " + email + " exists";
                        connection.Close();

                    }
                  
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