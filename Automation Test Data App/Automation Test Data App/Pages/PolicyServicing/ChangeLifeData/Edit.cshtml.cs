using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.ChangeLifeData
{
    public class EditModel : PageModel
    { 
    public ChangeLifeDataInfo ChangeLifeDataInfo = new ChangeLifeDataInfo();
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
                    String sql = "SELECT * FROM ChangeLifeData WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {

                                ChangeLifeDataInfo.id = String.Empty + reader["Scenario_ID"].ToString(); 
                                ChangeLifeDataInfo.Title = String.Empty + reader["Title"].ToString();
                                ChangeLifeDataInfo.Surname = String.Empty + reader["Surname"].ToString();
                                ChangeLifeDataInfo.MaritalStatus = String.Empty + reader["MaritalStatus"].ToString();
                                ChangeLifeDataInfo.EducationLevel = String.Empty + reader["EducationLevel"].ToString();
                                ChangeLifeDataInfo.Department = String.Empty + reader["Department"].ToString();
                                ChangeLifeDataInfo.Profession = String.Empty + reader["Profession"].ToString();
                                ChangeLifeDataInfo.Roleplayer = String.Empty + reader["Roleplayer"].ToString();
                                ChangeLifeDataInfo.RolePlayer_idNum = String.Empty + reader["RolePlayer_idNum"].ToString();
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
            ChangeLifeDataInfo.id = Request.Form["id"];
            ChangeLifeDataInfo.Title = Request.Form["Title"];
            ChangeLifeDataInfo.Surname = Request.Form["Surname"];
            ChangeLifeDataInfo.MaritalStatus = Request.Form["MaritalStatus"];
            ChangeLifeDataInfo.EducationLevel = Request.Form["EducationLevel"];
            ChangeLifeDataInfo.Department = Request.Form["Department"];
            ChangeLifeDataInfo.Profession = Request.Form["Profession"];
            ChangeLifeDataInfo.Roleplayer = Request.Form["Roleplayer"];
            ChangeLifeDataInfo.RolePlayer_idNum = Request.Form["RolePlayer_idNum"];


            if (ChangeLifeDataInfo.Title.Length == 0 || ChangeLifeDataInfo.Surname.Length == 0 || ChangeLifeDataInfo.MaritalStatus.Length == 0 ||
               ChangeLifeDataInfo.EducationLevel.Length == 0 || ChangeLifeDataInfo.Department.Length == 0 || ChangeLifeDataInfo.Profession.Length == 0
                || ChangeLifeDataInfo.Roleplayer.Length == 0 || ChangeLifeDataInfo.RolePlayer_idNum.Length == 0)
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
                    String sql = "UPDATE ChangeLifeData " +
                                 "SET Title=@Title, Surname=@Surname, MaritalStatus=@MaritalStatus, EducationLevel=@EducationLevel, Department=@Department, Profession=@Profession, RolePlayer = @RolePlayer, RolePlayer_idNum=@RolePlayer_idNum" +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Title", ChangeLifeDataInfo.Title);
                        command.Parameters.AddWithValue("@Surname", ChangeLifeDataInfo.Surname);
                        command.Parameters.AddWithValue("@MaritalStatus", ChangeLifeDataInfo.MaritalStatus);
                        command.Parameters.AddWithValue("@EducationLevel", ChangeLifeDataInfo.EducationLevel);
                        command.Parameters.AddWithValue("@Department", ChangeLifeDataInfo.Department);
                        command.Parameters.AddWithValue("@Profession", ChangeLifeDataInfo.Profession);
                        command.Parameters.AddWithValue("@Roleplayer", ChangeLifeDataInfo.Roleplayer);
                        command.Parameters.AddWithValue("@RolePlayer_idNum", ChangeLifeDataInfo.RolePlayer_idNum);


                        command.ExecuteNonQuery();

                    }
                    ChangeLifeDataInfo.Title = ""; ChangeLifeDataInfo.Surname = ""; ChangeLifeDataInfo.MaritalStatus = ""; 
                    ChangeLifeDataInfo.EducationLevel = ""; ChangeLifeDataInfo.Department = ""; ChangeLifeDataInfo.Profession = "";
                    ChangeLifeDataInfo.Roleplayer = ""; ChangeLifeDataInfo.RolePlayer_idNum = "";
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
