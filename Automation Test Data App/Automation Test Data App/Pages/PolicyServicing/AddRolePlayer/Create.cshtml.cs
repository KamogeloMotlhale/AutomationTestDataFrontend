using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.AddRolePlayer
{
    public class CreateModel : PageModel
    {
        public AddRolePlayerInfo AddRolePlayerInfo = new AddRolePlayerInfo(); 
        public String errorMessage = "";
        public String successMessage = "";
        public String scenarioID = "";

        public void OnGet()
        {
            string userId = Request.Cookies["UserID"];
            if (userId == null)
            {
                Response.Redirect("/");
            }
            else
            {
                scenarioID = Request.Query["scenarioid"];
                return;

            }
        }

        public void OnPost()
        {
            string userId = Request.Cookies["UserID"];
            scenarioID = Request.Query["scenarioid"].ToString();
            if (userId == null)
            {
                Response.Redirect("/");
            }
            else
            {
                AddRolePlayerInfo.Title = Request.Form["Title"];
                AddRolePlayerInfo.First_Name = Request.Form["First_Name"];
                AddRolePlayerInfo.Surname = Request.Form["Surname"];
                AddRolePlayerInfo.Initials = Request.Form["Initials"];
                AddRolePlayerInfo.Role_Type = Request.Form["RolePlayerType"];
                AddRolePlayerInfo.DOB = Request.Form["DOB"];
                AddRolePlayerInfo.Gender = Request.Form["Gender"];
                AddRolePlayerInfo.ID_number = Request.Form["ID_number"];
                AddRolePlayerInfo.Relationship = Request.Form["Relationship"];
                AddRolePlayerInfo.Comm_date = Request.Form["Comm_date"];
                AddRolePlayerInfo.Sum_Assured = Request.Form["Sum_Assured"];

                if (AddRolePlayerInfo.Title.Length == 0 || AddRolePlayerInfo.First_Name.Length == 0 || AddRolePlayerInfo.Surname.Length == 0 ||
                   AddRolePlayerInfo.Initials.Length == 0 || AddRolePlayerInfo.DOB.Length == 0 || AddRolePlayerInfo.Gender.Length == 0 ||
                   AddRolePlayerInfo.ID_number.Length == 0 || AddRolePlayerInfo.Relationship.Length == 0 || AddRolePlayerInfo.Comm_date.Length == 0 || AddRolePlayerInfo.Sum_Assured.Length == 0)
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
                        String sql = "INSERT INTO AddRolePlayer " +
                                    "(Scenario_ID, Title,RolePlayerType, First_Name, Surname, Initials, DOB, Gender, ID_number, Relationship, Comm_date, Sum_Assured) VALUES" +
                                    "(@id ,@Title,@RolePlayerType, @First_Name, @Surname, @Initials, @DOB, @Gender, @ID_number, @Relationship, @Comm_date , @Sum_Assured);";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@id", scenarioID);
                            command.Parameters.AddWithValue("@Title", AddRolePlayerInfo.Title);
                            command.Parameters.AddWithValue("@First_Name", AddRolePlayerInfo.First_Name);
                            command.Parameters.AddWithValue("@Surname", AddRolePlayerInfo.Surname);
                            command.Parameters.AddWithValue("@Initials", AddRolePlayerInfo.Initials);
                            command.Parameters.AddWithValue("@DOB", AddRolePlayerInfo.DOB);
                            command.Parameters.AddWithValue("@RolePlayerType", AddRolePlayerInfo.Role_Type);
                            command.Parameters.AddWithValue("@Gender", AddRolePlayerInfo.Gender);
                            command.Parameters.AddWithValue("@ID_number", AddRolePlayerInfo.ID_number);
                            command.Parameters.AddWithValue("@Relationship", AddRolePlayerInfo.Relationship);
                            command.Parameters.AddWithValue("@Comm_date", AddRolePlayerInfo.Comm_date);
                            command.Parameters.AddWithValue("@Sum_Assured", AddRolePlayerInfo.Sum_Assured);
                            command.ExecuteNonQuery();

                        }
                        AddRolePlayerInfo.Title = ""; AddRolePlayerInfo.First_Name = ""; AddRolePlayerInfo.Surname = ""; AddRolePlayerInfo.Initials = "";
                        AddRolePlayerInfo.DOB = ""; AddRolePlayerInfo.Role_Type = ""; AddRolePlayerInfo.Gender = ""; AddRolePlayerInfo.ID_number = ""; AddRolePlayerInfo.Relationship = ""; AddRolePlayerInfo.Comm_date = ""; AddRolePlayerInfo.Sum_Assured = "";
                        successMessage = "New role player Added Successfully";
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("FOREIGN KEY constraint"))
                        errorMessage = $"Test data is already added for this scenario {scenarioID}";
                    else
                        errorMessage = ex.Message;
                    return;
                }

            }

        }
    }
}
