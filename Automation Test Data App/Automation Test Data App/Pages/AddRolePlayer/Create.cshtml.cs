using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.AddRolePlayer
{
    public class CreateModel : PageModel
    {
        public AddRolePlayerInfo addRolePlayerInfo = new AddRolePlayerInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            addRolePlayerInfo.Title = Request.Form["Title"];
            addRolePlayerInfo.First_Name = Request.Form["First_Name"];
            addRolePlayerInfo.Surname = Request.Form["Surname"];
            addRolePlayerInfo.Initials = Request.Form["Initials"];
            addRolePlayerInfo.dob = Request.Form["dob"];
            addRolePlayerInfo.Gender = Request.Form["Gender"];
            addRolePlayerInfo.ID_number = Request.Form["ID_number"];
            addRolePlayerInfo.Relationship = Request.Form["Relationship"];
            addRolePlayerInfo.Comm_date = Request.Form["Comm_date"];

            if (addRolePlayerInfo.Title.Length == 0 || addRolePlayerInfo.First_Name.Length == 0 || addRolePlayerInfo.Surname.Length == 0 ||
               addRolePlayerInfo.Initials.Length == 0 || addRolePlayerInfo.dob.Length == 0 || addRolePlayerInfo.Gender.Length == 0 ||
               addRolePlayerInfo.ID_number.Length == 0 || addRolePlayerInfo.Relationship.Length == 0 || addRolePlayerInfo.Comm_date.Length == 0)
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
                                "(Title, First_Name, Surname, Initials, dob, Gender, ID_number, Relationship, Comm_date) VALUES" +
                                "(@Title, @First_Name, @Surname, @Initials, @dob, @Gender, @ID_number, @Relationship, @Comm_date);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Title", addRolePlayerInfo.Title);
                        command.Parameters.AddWithValue("@First_Name", addRolePlayerInfo.First_Name);
                        command.Parameters.AddWithValue("@Surname", addRolePlayerInfo.Surname);
                        command.Parameters.AddWithValue("@Initials", addRolePlayerInfo.Initials);
                        command.Parameters.AddWithValue("@dob", addRolePlayerInfo.dob);
                        command.Parameters.AddWithValue("@Gender", addRolePlayerInfo.Gender);
                        command.Parameters.AddWithValue("@ID_number", addRolePlayerInfo.ID_number);
                        command.Parameters.AddWithValue("@Relationship", addRolePlayerInfo.Relationship);
                        command.Parameters.AddWithValue("@Comm_date", addRolePlayerInfo.Comm_date);

                        command.ExecuteNonQuery();

                    }


                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            addRolePlayerInfo.Title = ""; addRolePlayerInfo.First_Name = ""; addRolePlayerInfo.Surname = ""; addRolePlayerInfo.Initials = ""; addRolePlayerInfo.dob = ""; addRolePlayerInfo.Gender = ""; addRolePlayerInfo.ID_number = ""; addRolePlayerInfo.Relationship = ""; addRolePlayerInfo.Comm_date = "";
            successMessage = "New Life Assured Added Successfully";
            Response.Redirect("/Additional Life Assured/Index");
        }
    }
}
