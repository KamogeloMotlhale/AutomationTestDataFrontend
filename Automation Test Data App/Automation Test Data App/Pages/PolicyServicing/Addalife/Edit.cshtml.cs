using Automation_Test_Data_App.Pages.Addalife;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.Addalife
{
    public class EditModel : PageModel
    { 
    public AddaLifeInfo AddaLifeInfo = new AddaLifeInfo();
    public String errorMessage = "";
    public String successMessage = "";

        public void OnGet()
        {
          

            String id = Request.Query["id"];

            try
            {
                string userId = Request.Cookies["UserID"];
                if (userId == null)
                {
                    Response.Redirect("/");
                }

                String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    String sql = "SELECT * FROM AddaLife WHERE id=@id"; 
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {

                                AddaLifeInfo.id = "" + reader.GetInt32(0);
                                AddaLifeInfo.Title = reader.GetString(1);
                                AddaLifeInfo.First_Name = reader.GetString(2);
                                AddaLifeInfo.Surname = reader.GetString(3);
                                AddaLifeInfo.Initials = reader.GetString(4);
                                AddaLifeInfo.dob = reader.GetString(5);
                                AddaLifeInfo.Gender = reader.GetString(6);
                                AddaLifeInfo.ID_number = reader.GetString(7);
                                AddaLifeInfo.Relationship = reader.GetString(8);
                                AddaLifeInfo.Comm_date = reader.GetString(9);
                                AddaLifeInfo.CoverAmount = reader.GetString(10);


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
            AddaLifeInfo.id = Request.Form["id"];
            AddaLifeInfo.Title = Request.Form["Title"];
            AddaLifeInfo.First_Name = Request.Form["First_Name"];
            AddaLifeInfo.Surname = Request.Form["Surname"];
            AddaLifeInfo.Initials = Request.Form["Initials"];
            AddaLifeInfo.dob = Request.Form["dob"];
            AddaLifeInfo.Gender = Request.Form["Gender"];
            AddaLifeInfo.ID_number = Request.Form["ID_number"];
            AddaLifeInfo.Relationship = Request.Form["Relationship"];
            AddaLifeInfo.Comm_date = Request.Form["Comm_date"];
            AddaLifeInfo.Comm_date = Request.Form["CoverAmount"];


            if (AddaLifeInfo.Title.Length == 0 || AddaLifeInfo.First_Name.Length == 0 || AddaLifeInfo.Surname.Length == 0 ||
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
                    String sql = "UPDATE AddaLife " +
                                 "SET Title=@Title, First_Name=@First_Name, Surname=@Surname, Initials=@Initials, dob=@dob, Gender=@Gender, ID_number=@ID_number, Relationship=@Relationship, Comm_date=@Comm_date, CoverAmount=@CoverAmount " +
                                 "WHERE id=@id";

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
                        command.Parameters.AddWithValue("@id", AddaLifeInfo.id);



                        command.ExecuteNonQuery();

                    }
                    AddaLifeInfo.Title = ""; AddaLifeInfo.First_Name = ""; AddaLifeInfo.Surname = ""; 
                    AddaLifeInfo.Initials = ""; AddaLifeInfo.dob = ""; AddaLifeInfo.Gender = ""; AddaLifeInfo.ID_number = ""; 
                    AddaLifeInfo.Relationship = ""; AddaLifeInfo.Comm_date = ""; AddaLifeInfo.CoverAmount = "";
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
