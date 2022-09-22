using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.ChangeLifeData
{
    public class CreateModel : PageModel
    {
        public ChangeLifeDataInfo ChangeLifeDataInfo = new ChangeLifeDataInfo(); 
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            ChangeLifeDataInfo.Title = Request.Form["Title"];
            ChangeLifeDataInfo.Surname = Request.Form["Surname"];
            ChangeLifeDataInfo.MaritalStatus = Request.Form["MaritalStatus"];
            ChangeLifeDataInfo.EducationLevel = Request.Form["EducationLevel"];
            ChangeLifeDataInfo.Department = Request.Form["Department"];
            ChangeLifeDataInfo.Profession = Request.Form["Profession"];
            

            if(ChangeLifeDataInfo.Title.Length == 0|| ChangeLifeDataInfo.Surname.Length == 0 || ChangeLifeDataInfo.MaritalStatus.Length == 0 ||
               ChangeLifeDataInfo.EducationLevel.Length == 0 || ChangeLifeDataInfo.Department.Length == 0 || ChangeLifeDataInfo.Profession.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
                  
            }
            //save new Life assured to DB
            try 
            {
               
                String connectionString = "Data Source=(LocalDB)/MSSQLLocalDB;AttachDbFilename=C:/Users/G992107/Documents/Github/ilrsafricanautopolicyservicing/data/Automation.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();
                    String sql = "INSERT INTO ChangeLifeData " +
                                "(Title, Surname, MaritalStatus, EducationLevel, Department, Profession) VALUES" +
                                "(@Title, @Surname, @MaritalStatus, @EducationLevel, @Department, @Profession);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Title", ChangeLifeDataInfo.Title);
                        command.Parameters.AddWithValue("@Surname", ChangeLifeDataInfo.Surname);
                        command.Parameters.AddWithValue("@MaritalStatus", ChangeLifeDataInfo.MaritalStatus);
                        command.Parameters.AddWithValue("@EducationLevel", ChangeLifeDataInfo.EducationLevel);
                        command.Parameters.AddWithValue("@Department", ChangeLifeDataInfo.Department);
                        command.Parameters.AddWithValue("@Profession", ChangeLifeDataInfo.Profession);
                      

                        command.ExecuteNonQuery();

                    }
                    ChangeLifeDataInfo.Title = ""; ChangeLifeDataInfo.Surname = ""; ChangeLifeDataInfo.MaritalStatus = ""; ChangeLifeDataInfo.EducationLevel = ""; ChangeLifeDataInfo.Department = ""; ChangeLifeDataInfo.Profession = ""; 
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
