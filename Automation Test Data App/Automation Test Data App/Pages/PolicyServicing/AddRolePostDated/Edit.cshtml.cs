using Automation_Test_Data_App.Pages.PolicyServicing.AddRolePostDated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.AddRolePostDated
{
    public class EditModel : PageModel
    { 
    public AddRolePlayerInfo AddRolePlayerInfo = new AddRolePlayerInfo();
    public String errorMessage = "";
    public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {

                String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    String sql = "SELECT * FROM AddRolePostDated WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {

                                AddRolePlayerInfo.id = "" + reader.GetInt32(0);
                                AddRolePlayerInfo.Title = reader.GetString(1);
                                AddRolePlayerInfo.First_Name = reader.GetString(2);
                                AddRolePlayerInfo.Surname = reader.GetString(3);
                                AddRolePlayerInfo.Initials = reader.GetString(4);
                                AddRolePlayerInfo.dob = reader.GetString(5);
                                AddRolePlayerInfo.Gender = reader.GetString(6);
                                AddRolePlayerInfo.ID_number = reader.GetString(7);
                                AddRolePlayerInfo.Relationship = reader.GetString(8);
                                AddRolePlayerInfo.Comm_date = reader.GetString(9);

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
            AddRolePlayerInfo.id = Request.Form["id"];
            AddRolePlayerInfo.Title = Request.Form["Title"];
            AddRolePlayerInfo.First_Name = Request.Form["First_Name"];
            AddRolePlayerInfo.Surname = Request.Form["Surname"];
            AddRolePlayerInfo.Initials = Request.Form["Initials"];
            AddRolePlayerInfo.dob = Request.Form["dob"];
            AddRolePlayerInfo.Gender = Request.Form["Gender"];
            AddRolePlayerInfo.ID_number = Request.Form["ID_number"];
            AddRolePlayerInfo.Relationship = Request.Form["Relationship"];
            AddRolePlayerInfo.Comm_date = Request.Form["Comm_date"];

            if (AddRolePlayerInfo.Title.Length == 0 || AddRolePlayerInfo.First_Name.Length == 0 || AddRolePlayerInfo.Surname.Length == 0 ||
               AddRolePlayerInfo.Initials.Length == 0 || AddRolePlayerInfo.dob.Length == 0 || AddRolePlayerInfo.Gender.Length == 0 ||
               AddRolePlayerInfo.ID_number.Length == 0 || AddRolePlayerInfo.Relationship.Length == 0 || AddRolePlayerInfo.Comm_date.Length == 0)
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
                    String sql = "UPDATE AddRolePostDated " +
                                 "SET Title=@Title, First_Name=@First_Name, Surname=@Surname, Initials=@Initials, dob=@dob, Gender=@Gender, ID_number=@ID_number, Relationship=@Relationship, Comm_date=@Comm_date " +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Title", AddRolePlayerInfo.Title);
                        command.Parameters.AddWithValue("@First_Name", AddRolePlayerInfo.First_Name);
                        command.Parameters.AddWithValue("@Surname", AddRolePlayerInfo.Surname);
                        command.Parameters.AddWithValue("@Initials", AddRolePlayerInfo.Initials);
                        command.Parameters.AddWithValue("@dob", AddRolePlayerInfo.dob);
                        command.Parameters.AddWithValue("@Gender", AddRolePlayerInfo.Gender);
                        command.Parameters.AddWithValue("@ID_number", AddRolePlayerInfo.ID_number);
                        command.Parameters.AddWithValue("@Relationship", AddRolePlayerInfo.Relationship);
                        command.Parameters.AddWithValue("@Comm_date", AddRolePlayerInfo.Comm_date);
                        command.Parameters.AddWithValue("@id", AddRolePlayerInfo.id);



                        command.ExecuteNonQuery();

                    }
                    AddRolePlayerInfo.Title = ""; AddRolePlayerInfo.First_Name = ""; AddRolePlayerInfo.Surname = ""; AddRolePlayerInfo.Initials = ""; AddRolePlayerInfo.dob = ""; AddRolePlayerInfo.Gender = ""; AddRolePlayerInfo.ID_number = ""; AddRolePlayerInfo.Relationship = ""; AddRolePlayerInfo.Comm_date = "";
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
