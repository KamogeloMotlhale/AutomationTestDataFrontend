using Automation_Test_Data_App.Pages.AddRolePlayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.AddRolePlayer
{
    public class EditModel : PageModel
    { 
    public AddRolePlayerInfo AddRolePlayerInfo = new AddRolePlayerInfo();
    public String errorMessage = "";
    public String successMessage = "";

        public void OnGet()
        {
          

            String id = Request.Query["scenarioid"];

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
                    String sql = "SELECT * FROM AddRolePlayer WHERE Scenario_ID=@id"; 
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {

                                AddRolePlayerInfo.Scenario_ID = reader["Scenario_ID"].ToString();
                                AddRolePlayerInfo.Title = reader["Title"].ToString();
                                AddRolePlayerInfo.First_Name = reader["First_Name"].ToString();
                                AddRolePlayerInfo.Surname = reader["Surname"].ToString();
                                AddRolePlayerInfo.Initials = reader["Initials"].ToString();
                                AddRolePlayerInfo.Role_Type = reader["RolePlayerType"].ToString();
                                AddRolePlayerInfo.DOB = reader["DOB"].ToString();
                                AddRolePlayerInfo.Gender = reader["Gender"].ToString();
                                AddRolePlayerInfo.ID_number = reader["ID_number"].ToString();
                                AddRolePlayerInfo.Relationship = reader["Relationship"].ToString();
                                AddRolePlayerInfo.Comm_date = reader["Comm_date"].ToString();
                                AddRolePlayerInfo.Sum_Assured = reader["Sum_Assured"].ToString();


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
            
            AddRolePlayerInfo.Title = Request.Form["Title"];
            AddRolePlayerInfo.First_Name = Request.Form["First_Name"];
            AddRolePlayerInfo.Surname = Request.Form["Surname"];
            AddRolePlayerInfo.Initials = Request.Form["Initials"];
            AddRolePlayerInfo.Role_Type = Request.Form["rolePlayerType"];
            AddRolePlayerInfo.DOB = Request.Form["DOB"];
            AddRolePlayerInfo.Gender = Request.Form["Gender"];
            AddRolePlayerInfo.ID_number = Request.Form["ID_number"];
            AddRolePlayerInfo.Relationship = Request.Form["Relationship"];
            AddRolePlayerInfo.Comm_date = Request.Form["Comm_date"];
            AddRolePlayerInfo.Sum_Assured = Request.Form["Sum_Assured"];


            if (AddRolePlayerInfo.Role_Type.Length == 0||AddRolePlayerInfo.Title.Length == 0 || AddRolePlayerInfo.First_Name.Length == 0 || AddRolePlayerInfo.Surname.Length == 0 ||
               AddRolePlayerInfo.Initials.Length == 0 || AddRolePlayerInfo.DOB.Length == 0 || AddRolePlayerInfo.Gender.Length == 0 ||
               AddRolePlayerInfo.ID_number.Length == 0 || AddRolePlayerInfo.Relationship.Length == 0 || AddRolePlayerInfo.Comm_date.Length == 0 || AddRolePlayerInfo.Sum_Assured.Length == 0 )
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
                    String sql = "UPDATE AddRolePlayer " +
                                 "SET Title=@Title, First_Name=@First_Name,RolePlayerType=@Role_type, Surname=@Surname, Initials=@Initials, DOB=@DOB, Gender=@Gender, ID_number=@ID_number, Relationship=@Relationship, Comm_date=@Comm_date, Sum_Assured=@Sum_Assured " +
                                 "WHERE Scenario_ID=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Title", AddRolePlayerInfo.Title);
                        command.Parameters.AddWithValue("@First_Name", AddRolePlayerInfo.First_Name);
                        command.Parameters.AddWithValue("@Surname", AddRolePlayerInfo.Surname);
                        command.Parameters.AddWithValue("@Initials", AddRolePlayerInfo.Initials);
                        command.Parameters.AddWithValue("@DOB", AddRolePlayerInfo.DOB);
                        command.Parameters.AddWithValue("@Role_type", AddRolePlayerInfo.Role_Type);
                        command.Parameters.AddWithValue("@Gender", AddRolePlayerInfo.Gender);
                        command.Parameters.AddWithValue("@ID_number", AddRolePlayerInfo.ID_number);
                        command.Parameters.AddWithValue("@Relationship", AddRolePlayerInfo.Relationship);
                        command.Parameters.AddWithValue("@Comm_date", AddRolePlayerInfo.Comm_date);
                        command.Parameters.AddWithValue("@Sum_Assured", AddRolePlayerInfo.Sum_Assured);
                        command.Parameters.AddWithValue("@id", AddRolePlayerInfo.ID_number);



                        command.ExecuteNonQuery();

                    }
                    AddRolePlayerInfo.Title = ""; AddRolePlayerInfo.First_Name = ""; AddRolePlayerInfo.Surname = "";
                    AddRolePlayerInfo.Initials = ""; AddRolePlayerInfo.DOB = ""; AddRolePlayerInfo.Gender = ""; AddRolePlayerInfo.ID_number = "";
                    AddRolePlayerInfo.Relationship = ""; AddRolePlayerInfo.Comm_date = ""; AddRolePlayerInfo.Sum_Assured = "";

                    successMessage = "AddRolePlayerInfo Updated Successfully";

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
