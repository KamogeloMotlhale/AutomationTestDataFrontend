using Automation_Test_Data_App.Pages.AddRolePostDated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.AddRolePostDated
{
    public class EditModel : PageModel
    { 
    public AddRolePostDatedInfo AddRolePostDatedInfo = new AddRolePostDatedInfo();
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
                    String sql = "SELECT * FROM AddRolePostDated WHERE id=@id"; 
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {

                                AddRolePostDatedInfo.id = "" + reader.GetInt32(0);
                                AddRolePostDatedInfo.Title = reader.GetString(1);
                                AddRolePostDatedInfo.First_Name = reader.GetString(2);
                                AddRolePostDatedInfo.Surname = reader.GetString(3);
                                AddRolePostDatedInfo.Initials = reader.GetString(4);
                                AddRolePostDatedInfo.DOB = reader.GetString(5);
                                AddRolePostDatedInfo.Gender = reader.GetString(6);
                                AddRolePostDatedInfo.ID_number = reader.GetString(7);
                                AddRolePostDatedInfo.Relationship = reader.GetString(8);
                                AddRolePostDatedInfo.Comm_date = reader.GetString(9);
                                AddRolePostDatedInfo.CoverAmount = reader.GetString(10);


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
            AddRolePostDatedInfo.id = Request.Form["id"];
            AddRolePostDatedInfo.Title = Request.Form["Title"];
            AddRolePostDatedInfo.First_Name = Request.Form["First_Name"];
            AddRolePostDatedInfo.Surname = Request.Form["Surname"];
            AddRolePostDatedInfo.Initials = Request.Form["Initials"];
            AddRolePostDatedInfo.DOB = Request.Form["DOB"];
            AddRolePostDatedInfo.Gender = Request.Form["Gender"];
            AddRolePostDatedInfo.ID_number = Request.Form["ID_number"];
            AddRolePostDatedInfo.Relationship = Request.Form["Relationship"];
            AddRolePostDatedInfo.Comm_date = Request.Form["Comm_date"];
            AddRolePostDatedInfo.CoverAmount = Request.Form["CoverAmount"];


            if (AddRolePostDatedInfo.Title.Length == 0 || AddRolePostDatedInfo.First_Name.Length == 0 || AddRolePostDatedInfo.Surname.Length == 0 ||
               AddRolePostDatedInfo.Initials.Length == 0 || AddRolePostDatedInfo.DOB.Length == 0 || AddRolePostDatedInfo.Gender.Length == 0 ||
               AddRolePostDatedInfo.ID_number.Length == 0 || AddRolePostDatedInfo.Relationship.Length == 0 || AddRolePostDatedInfo.Comm_date.Length == 0 || AddRolePostDatedInfo.CoverAmount.Length == 0)
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
                                 "SET Title=@Title, First_Name=@First_Name, Surname=@Surname, Initials=@Initials, DOB=@DOB, Gender=@Gender, ID_number=@ID_number, Relationship=@Relationship, Comm_date=@Comm_date, CoverAmount=@CoverAmount " +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Title", AddRolePostDatedInfo.Title);
                        command.Parameters.AddWithValue("@First_Name", AddRolePostDatedInfo.First_Name);
                        command.Parameters.AddWithValue("@Surname", AddRolePostDatedInfo.Surname);
                        command.Parameters.AddWithValue("@Initials", AddRolePostDatedInfo.Initials);
                        command.Parameters.AddWithValue("@DOB", AddRolePostDatedInfo.DOB);
                        command.Parameters.AddWithValue("@Gender", AddRolePostDatedInfo.Gender);
                        command.Parameters.AddWithValue("@ID_number", AddRolePostDatedInfo.ID_number);
                        command.Parameters.AddWithValue("@Relationship", AddRolePostDatedInfo.Relationship);
                        command.Parameters.AddWithValue("@Comm_date", AddRolePostDatedInfo.Comm_date);
                        command.Parameters.AddWithValue("@CoverAmount", AddRolePostDatedInfo.CoverAmount);
                        command.Parameters.AddWithValue("@id", AddRolePostDatedInfo.id);



                        command.ExecuteNonQuery();

                    }
                    AddRolePostDatedInfo.Title = ""; AddRolePostDatedInfo.First_Name = ""; AddRolePostDatedInfo.Surname = "";
                    AddRolePostDatedInfo.Initials = ""; AddRolePostDatedInfo.DOB = ""; AddRolePostDatedInfo.Gender = ""; AddRolePostDatedInfo.ID_number = "";
                    AddRolePostDatedInfo.Relationship = ""; AddRolePostDatedInfo.Comm_date = ""; AddRolePostDatedInfo.CoverAmount = "";

                    successMessage = "Add Postdated role player details edited sucessfully";

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
