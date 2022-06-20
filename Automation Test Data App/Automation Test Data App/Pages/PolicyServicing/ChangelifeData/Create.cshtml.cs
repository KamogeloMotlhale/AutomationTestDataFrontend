using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.ChangelifeData
{
    public class CreateModel : PageModel
    {
        public ChangelifeDataInfo ChangelifeDataInfo = new ChangelifeDataInfo(); 
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            ChangelifeDataInfo.Title = Request.Form["Title"];
            ChangelifeDataInfo.Surname = Request.Form["Surname"];
            ChangelifeDataInfo.MaritalStatus = Request.Form["MaritalStatus"];
            ChangelifeDataInfo.EducationLevel = Request.Form["EducationLevel"];
            ChangelifeDataInfo.Department = Request.Form["Department"];
            ChangelifeDataInfo.Profession = Request.Form["Profession"];
           
            if(ChangelifeDataInfo.Title.Length == 0 || ChangelifeDataInfo.Surname.Length == 0 ||
               ChangelifeDataInfo.MaritalStatus.Length == 0 || ChangelifeDataInfo.EducationLevel.Length == 0 || ChangelifeDataInfo.Department.Length == 0 ||
               ChangelifeDataInfo.Profession.Length == 0 )
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
                    String sql = "INSERT INTO ChangelifeData " +
                                "(Title,  Surname, MaritalStatus, EducationLevel, Department, Profession) VALUES" +
                                "(@Title, @Surname, @MaritalStatus, @EducationLevel, @Department, @Profession);";


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@Title", ChangelifeDataInfo.Title);
                        command.Parameters.AddWithValue("@Surname", ChangelifeDataInfo.Surname);
                        command.Parameters.AddWithValue("@MaritalStatus", ChangelifeDataInfo.MaritalStatus);
                        command.Parameters.AddWithValue("@EducationLevel", ChangelifeDataInfo.EducationLevel);
                        command.Parameters.AddWithValue("@Department", ChangelifeDataInfo.Department);
                        command.Parameters.AddWithValue("@Profession", ChangelifeDataInfo.Profession);
                    
                        command.ExecuteNonQuery();

                    }
                }

            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            ChangelifeDataInfo.Title = "";  ChangelifeDataInfo.Surname = ""; ChangelifeDataInfo.MaritalStatus = ""; ChangelifeDataInfo.EducationLevel = ""; ChangelifeDataInfo.Department = ""; ChangelifeDataInfo.Profession = ""; 
            successMessage = "New Life Assured Added Successfully";
            Response.Redirect("/Additional Life Assured/Index");
        }
    }
}
