using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.Addalife
{
    public class CreateModel : PageModel
    {
        public AddaLifeInfo AddaLifeInfo = new AddaLifeInfo(); 
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            string userId = Request.Cookies["UserID"];
            if (userId == null)
            {
                Response.Redirect("/");
            }
            else
            {
                return;

            }
        }

        public void OnPost()
        { 
            AddaLifeInfo.Title = Request.Form["Title"];
            AddaLifeInfo.First_Name = Request.Form["First_Name"];
            AddaLifeInfo.Surname = Request.Form["Surname"];
            AddaLifeInfo.Initials = Request.Form["Initials"];
            AddaLifeInfo.dob = Request.Form["dob"];
            AddaLifeInfo.Gender = Request.Form["Gender"];
            AddaLifeInfo.ID_number = Request.Form["ID_number"];
            AddaLifeInfo.Relationship = Request.Form["Relationship"];
            AddaLifeInfo.Comm_date = Request.Form["Comm_date"];
            AddaLifeInfo.CoverAmount = Request.Form["CoverAmount"];


            if (AddaLifeInfo.Title.Length == 0|| AddaLifeInfo.First_Name.Length == 0 || AddaLifeInfo.Surname.Length == 0 ||           
               AddaLifeInfo.Initials.Length == 0 || AddaLifeInfo.dob.Length == 0 || AddaLifeInfo.Gender.Length == 0 ||
               AddaLifeInfo.ID_number.Length == 0 || AddaLifeInfo.Relationship.Length == 0 || AddaLifeInfo.Comm_date.Length == 0 || AddaLifeInfo.CoverAmount.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
                  
            }
            //save new Life assured to DB
            try 
            {
               
                String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();
                    String sql = "INSERT INTO AddaLife " +
                                "(Title, First_Name, Surname, Initials, dob, Gender, ID_number, Relationship, Comm_date, CoverAmount) VALUES" +
                                "(@Title, @First_Name, @Surname, @Initials, @dob, @Gender, @ID_number, @Relationship, @Comm_date , @CoverAmount);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Title", AddaLifeInfo.Title);
                        command.Parameters.AddWithValue("@First_Name", AddaLifeInfo.First_Name);
                        command.Parameters.AddWithValue("@Surname", AddaLifeInfo.Surname);
                        command.Parameters.AddWithValue("@Initials", AddaLifeInfo.Initials);
                        command.Parameters.AddWithValue("@dob", AddaLifeInfo.dob);
                        command.Parameters.AddWithValue("@Gender", AddaLifeInfo.Gender);
                        command.Parameters.AddWithValue("@ID_number", AddaLifeInfo.ID_number);
                        command.Parameters.AddWithValue("@Relationship", AddaLifeInfo.Relationship);
                        command.Parameters.AddWithValue("@Comm_date", AddaLifeInfo.Comm_date);
                        command.Parameters.AddWithValue("@CoverAmount", AddaLifeInfo.CoverAmount);


                        command.ExecuteNonQuery();

                    }
                    AddaLifeInfo.Title = ""; AddaLifeInfo.First_Name = ""; AddaLifeInfo.Surname = ""; AddaLifeInfo.Initials = ""; 
                    AddaLifeInfo.dob = ""; AddaLifeInfo.Gender = ""; AddaLifeInfo.ID_number = ""; AddaLifeInfo.Relationship = ""; AddaLifeInfo.Comm_date = ""; AddaLifeInfo.CoverAmount = "";
                    successMessage = "New Life Assured Added Successfully";
                    
                }

            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

        }
    }
}
