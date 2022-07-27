using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyHolder_Details
{
    public class CreateModel : PageModel
    {
        public PolicyHolder_DetailsInfo PolicyHolder_DetailsInfo = new PolicyHolder_DetailsInfo(); 
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            PolicyHolder_DetailsInfo.Scenario_ID = Request.Form["Scenario_ID"];
            PolicyHolder_DetailsInfo.Town = Request.Form["Town"];
            PolicyHolder_DetailsInfo.Worksite = Request.Form["Worksite"];
            PolicyHolder_DetailsInfo.Employment = Request.Form["Employment"];
            PolicyHolder_DetailsInfo.First_name = Request.Form["First_name"];
            PolicyHolder_DetailsInfo.Maiden_Surname = Request.Form["Maiden_Surname"];
            PolicyHolder_DetailsInfo.Surname = Request.Form["Surname"];
            PolicyHolder_DetailsInfo.ID_number = Request.Form["ID_number"];
            PolicyHolder_DetailsInfo.Ethnicity = Request.Form["Ethnicity"];
            PolicyHolder_DetailsInfo.Marital_Status = Request.Form["Marital_Status"]; 
            PolicyHolder_DetailsInfo.CellPhone_number = Request.Form["CellPhone_number"];
            PolicyHolder_DetailsInfo.Email = Request.Form["Email"];
            PolicyHolder_DetailsInfo.Nationality = Request.Form["Nationality"];
            PolicyHolder_DetailsInfo.Country_Of_Birth = Request.Form["Country_Of_Birth"];
            PolicyHolder_DetailsInfo.Country_Of_Residence = Request.Form["Country_Of_Residence"];
            PolicyHolder_DetailsInfo.Gross = Request.Form["Gross"];
            PolicyHolder_DetailsInfo.Permanent = Request.Form["Permanent"];
            PolicyHolder_DetailsInfo.Salary_frequency = Request.Form["Salary_frequency"];
            PolicyHolder_DetailsInfo.Gender = Request.Form["Gender"];
            PolicyHolder_DetailsInfo.DOB = Request.Form["DOB"];
            PolicyHolder_DetailsInfo.Card_Number = Request.Form["Card_Number"];
            PolicyHolder_DetailsInfo.Covered = Request.Form["Covered"];
            PolicyHolder_DetailsInfo.Cover_Amount = Request.Form["Cover_Amount"];

            if (PolicyHolder_DetailsInfo.Scenario_ID.Length == 0|| PolicyHolder_DetailsInfo.Town.Length == 0 || PolicyHolder_DetailsInfo.Town.Length == 0 || PolicyHolder_DetailsInfo.Worksite.Length == 0 ||
               PolicyHolder_DetailsInfo.First_name.Length == 0 || PolicyHolder_DetailsInfo.Maiden_Surname.Length == 0 || PolicyHolder_DetailsInfo.Surname.Length == 0 ||
               PolicyHolder_DetailsInfo.ID_number.Length == 0 || PolicyHolder_DetailsInfo.Ethnicity.Length == 0 || PolicyHolder_DetailsInfo.Marital_Status.Length == 0 || 
               PolicyHolder_DetailsInfo.CellPhone_number.Length == 0 || PolicyHolder_DetailsInfo.Email.Length == 0 || PolicyHolder_DetailsInfo.Nationality.Length == 0 ||
               PolicyHolder_DetailsInfo.Country_Of_Birth.Length == 0 || PolicyHolder_DetailsInfo.Country_Of_Residence.Length == 0 || PolicyHolder_DetailsInfo.Gross.Length == 0 ||
               PolicyHolder_DetailsInfo.Permanent.Length == 0 || PolicyHolder_DetailsInfo.Salary_frequency.Length == 0 || PolicyHolder_DetailsInfo.Gender.Length == 0 ||
               PolicyHolder_DetailsInfo.DOB.Length == 0 || PolicyHolder_DetailsInfo.Card_Number.Length == 0 || PolicyHolder_DetailsInfo.Covered.Length == 0 || PolicyHolder_DetailsInfo.Cover_Amount.Length == 0)
            {
                errorMessage = "All the fie                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           lds are required";
                return;
                  
            }
            //save new Life assured to DB
            try 
            {
               
                String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();
                    String sql = "INSERT INTO PolicyHolder_Details " +
                                "(Scenario_ID, Town, Worksite, Employment, Maiden_Surname, ID_number, Ethnicity, Marital_Status, CellPhone_number, Email, Nationality, Country_Of_Birth, Country_Of_Residence, Gross, Permanent, Salary_frequency, Gender, DOB, Card_Number, Covered, Cover_Amount) VALUES" +
                                "(@Scenario_ID, @Town, @Worksite, @Employment, @Maiden_Surname, @ID_number, @Ethnicity, @Marital_Status, @CellPhone_number, @Country_Of_Birth, @Nationality, @Country_Of_Birth, @Country_Of_Residence, @Gross, @Permanent, @Salary_frequency, @Gender, @DOB, @Card_Number, @Covered, @Cover_Amount);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Scenario_ID", PolicyHolder_DetailsInfo.Scenario_ID);
                        command.Parameters.AddWithValue("@Town ", PolicyHolder_DetailsInfo.Town);
                        command.Parameters.AddWithValue("@Worksite", PolicyHolder_DetailsInfo.Worksite);
                        command.Parameters.AddWithValue("@Employment", PolicyHolder_DetailsInfo.Employment);
                        command.Parameters.AddWithValue("@First_name", PolicyHolder_DetailsInfo.First_name);
                        command.Parameters.AddWithValue("@Maiden_Surname", PolicyHolder_DetailsInfo.Maiden_Surname);
                        command.Parameters.AddWithValue("@Surname", PolicyHolder_DetailsInfo.Surname);
                        command.Parameters.AddWithValue("@ID_number", PolicyHolder_DetailsInfo.ID_number);
                        command.Parameters.AddWithValue("@Ethnicity", PolicyHolder_DetailsInfo.Ethnicity);
                        command.Parameters.AddWithValue("@Marital_Status", PolicyHolder_DetailsInfo.Marital_Status);
                        command.Parameters.AddWithValue("@CellPhone_number", PolicyHolder_DetailsInfo.CellPhone_number);
                        command.Parameters.AddWithValue("@Email", PolicyHolder_DetailsInfo.Email);
                        command.Parameters.AddWithValue("@Nationality", PolicyHolder_DetailsInfo.Nationality);
                        command.Parameters.AddWithValue("@Country_Of_Birth", PolicyHolder_DetailsInfo.Country_Of_Birth);
                        command.Parameters.AddWithValue("@Country_Of_Residence", PolicyHolder_DetailsInfo.Country_Of_Residence);
                        command.Parameters.AddWithValue("@Gross", PolicyHolder_DetailsInfo.Gross);
                        command.Parameters.AddWithValue("@Permanent", PolicyHolder_DetailsInfo.Permanent);
                        command.Parameters.AddWithValue("@Salary_frequency", PolicyHolder_DetailsInfo.Salary_frequency); 
                        command.Parameters.AddWithValue("@Gender", PolicyHolder_DetailsInfo.Gender);
                        command.Parameters.AddWithValue("@DOB", PolicyHolder_DetailsInfo.DOB);
                        command.Parameters.AddWithValue("@Card_Number", PolicyHolder_DetailsInfo.Card_Number);
                        command.Parameters.AddWithValue("@Covered", PolicyHolder_DetailsInfo.Covered);
                        command.Parameters.AddWithValue("@Cover_Amount", PolicyHolder_DetailsInfo.Cover_Amount);
                        


                        command.ExecuteNonQuery();

                    }
                    PolicyHolder_DetailsInfo.Scenario_ID = ""; PolicyHolder_DetailsInfo.Town = ""; PolicyHolder_DetailsInfo.Worksite = "";
                    PolicyHolder_DetailsInfo.Employment = ""; PolicyHolder_DetailsInfo.First_name = ""; PolicyHolder_DetailsInfo.Maiden_Surname = "";
                    PolicyHolder_DetailsInfo.Surname = ""; PolicyHolder_DetailsInfo.ID_number = ""; PolicyHolder_DetailsInfo.Ethnicity = "";
                    PolicyHolder_DetailsInfo.Marital_Status = ""; PolicyHolder_DetailsInfo.CellPhone_number = ""; PolicyHolder_DetailsInfo.Email = "";
                    PolicyHolder_DetailsInfo.Nationality = ""; PolicyHolder_DetailsInfo.Country_Of_Birth = ""; PolicyHolder_DetailsInfo.Country_Of_Residence = "";
                    PolicyHolder_DetailsInfo.Gross = ""; PolicyHolder_DetailsInfo.Permanent = ""; PolicyHolder_DetailsInfo.Salary_frequency = "";
                    PolicyHolder_DetailsInfo.Gender = ""; PolicyHolder_DetailsInfo.DOB = ""; PolicyHolder_DetailsInfo.Card_Number = "";
                    PolicyHolder_DetailsInfo.Covered = ""; PolicyHolder_DetailsInfo.Cover_Amount = "";
                    
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
 