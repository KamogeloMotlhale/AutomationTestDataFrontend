using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.Affordability_Check
{
    public class CreateModel : PageModel
    {
        public PolicyHolderInfo PolicyHolderInfo = new PolicyHolderInfo(); 
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            AffordabilityCheckInfo.AffordabilityCheck = Request.Form["Date"];
            AffordabilityCheckInfo.Component = Request.Form["Component"];
            AffordabilityCheckInfo.Cover_Amount = Request.Form["Cover_Amount"];
           

            if(AffordabilityCheckInfo.Date.Length == 0|| AffordabilityCheckInfo.Component.Length == 0 || AffordabilityCheckInfo.Cover_Amount.Length == 0)
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
                    String sql = "INSERT INTO PolicyHolderInfo_Details " +
                                "(Scenario_ID, Component, Cover_Amount) VALUES" +
                                "(@Scenario_ID, @Component, @Cover_Amount);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Date", PolicyHolderInfo.Scenario_ID);
                        command.Parameters.AddWithValue("@Component", PolicyHolderInfo.Town);
                        command.Parameters.AddWithValue("@Cover_Amount", PolicyHolderInfo.Worksite);
                        command.Parameters.AddWithValue("@Cover_Amount", PolicyHolderInfo.Employment);
                        command.Parameters.AddWithValue("@Cover_Amount", PolicyHolderInfo.First_name);
                        command.Parameters.AddWithValue("@Cover_Amount", PolicyHolderInfo.Maiden_Surname);
                        command.Parameters.AddWithValue("@Cover_Amount", PolicyHolderInfo.Surname);
                        command.Parameters.AddWithValue("@Cover_Amount", PolicyHolderInfo.ID_number);
                        command.Parameters.AddWithValue("@Cover_Amount", PolicyHolderInfo.Ethnicity);
                        command.Parameters.AddWithValue("@Cover_Amount", PolicyHolderInfo.Marital_Status);
                        command.Parameters.AddWithValue("@Cover_Amount", PolicyHolderInfo.CellPhone_number);
                        command.Parameters.AddWithValue("@Cover_Amount", PolicyHolderInfo.Email);
                        command.Parameters.AddWithValue("@Cover_Amount", PolicyHolderInfo.Nationality);
                        command.Parameters.AddWithValue("@Cover_Amount", PolicyHolderInfo.Country_Of_Residence);
                        command.Parameters.AddWithValue("@Cover_Amount", PolicyHolderInfo.Gross);
                        command.Parameters.AddWithValue("@Cover_Amount", PolicyHolderInfo.Permanent);
                        command.Parameters.AddWithValue("@Cover_Amount", PolicyHolderInfo.Salary_frequency);
                        command.Parameters.AddWithValue("@Cover_Amount", PolicyHolderInfo.Covered);
                        command.Parameters.AddWithValue("@Cover_Amount", PolicyHolderInfo.Cover_Amount);



                        command.ExecuteNonQuery();

                    }


                }

            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            PolicyHolderInfo.Scenario_ID = ""; PolicyHolderInfo.Town = ""; PolicyHolderInfo.Worksite = "";
            PolicyHolderInfo.Employment = ""; PolicyHolderInfo.First_name = ""; PolicyHolderInfo.Maiden_Surname = "";
            PolicyHolderInfo.Surname = ""; PolicyHolderInfo.ID_number = ""; PolicyHolderInfo.Ethnicity = "";
            PolicyHolderInfo.CellPhone_number = ""; PolicyHolderInfo.Email = ""; PolicyHolderInfo.Nationality = "";
            PolicyHolderInfo.Country_Of_Residence = ""; PolicyHolderInfo.Gross = ""; PolicyHolderInfo.Permanent = "";
            PolicyHolderInfo.Salary_frequency = ""; PolicyHolderInfo.Covered = ""; PolicyHolderInfo.Cover_Amount = "";
            successMessage = "New Downgrade Component Added Successfully";
            Response.Redirect("/PolicyServicing/Downgrade");
        }
    }
}
