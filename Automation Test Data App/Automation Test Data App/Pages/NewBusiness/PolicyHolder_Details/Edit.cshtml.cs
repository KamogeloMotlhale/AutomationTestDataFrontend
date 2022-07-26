using Automation_Test_Data_App.Pages.NewBusiness.PolicyHolder_Details;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.NewBusiness.PolicyHolder_Details
{
    public class EditModel : PageModel
    {
        public PolicyHolder_DetailsInfo PolicyHolder_DetailsInfo = new PolicyHolder_DetailsInfo();
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
                    String sql = "SELECT * FROM PolicyHolder_Details WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {

                                PolicyHolder_DetailsInfo.id = "" + reader.GetInt32(0);
                                PolicyHolder_DetailsInfo.Scenario_ID = reader.GetString(1);
                                PolicyHolder_DetailsInfo.Town = reader.GetString(2);
                                PolicyHolder_DetailsInfo.Employment = reader.GetString(3);
                                PolicyHolder_DetailsInfo.First_name = reader.GetString(4);
                                PolicyHolder_DetailsInfo.Maiden_Surname = reader.GetString(5);
                                PolicyHolder_DetailsInfo.Surname = reader.GetString(6);
                                PolicyHolder_DetailsInfo.ID_number = reader.GetString(7);
                                PolicyHolder_DetailsInfo.Ethnicity = reader.GetString(8);
                                PolicyHolder_DetailsInfo.Marital_Status = reader.GetString(9);
                                PolicyHolder_DetailsInfo.CellPhone_number = reader.GetString(10);
                                PolicyHolder_DetailsInfo.Email = reader.GetString(11);
                                PolicyHolder_DetailsInfo.Nationality = reader.GetString(12);
                                PolicyHolder_DetailsInfo.Country_Of_Birth = reader.GetString(13);
                                PolicyHolder_DetailsInfo.Country_Of_Residence = reader.GetString(14);
                                PolicyHolder_DetailsInfo.Gross = reader.GetString(15);
                                PolicyHolder_DetailsInfo.Permanent = reader.GetString(16);
                                PolicyHolder_DetailsInfo.Salary_frequency = reader.GetString(17);
                                PolicyHolder_DetailsInfo.Gender = reader.GetString(18);
                                PolicyHolder_DetailsInfo.DOB = reader.GetString(19);
                                PolicyHolder_DetailsInfo.Card_Number = reader.GetString(21);
                                PolicyHolder_DetailsInfo.Covered = reader.GetString(22);
                                PolicyHolder_DetailsInfo.Cover_Amount = reader.GetString(23);



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
            PolicyHolder_DetailsInfo.id = Request.Form["id"];
            PolicyHolder_DetailsInfo.Scenario_ID = Request.Form["Scenario_ID"];
            PolicyHolder_DetailsInfo.Town = Request.Form["Town"];
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


            if (PolicyHolder_DetailsInfo.Scenario_ID.Length == 0 || PolicyHolder_DetailsInfo.Town.Length == 0 || PolicyHolder_DetailsInfo.Worksite.Length == 0 ||
                PolicyHolder_DetailsInfo.Employment.Length == 0 || PolicyHolder_DetailsInfo.First_name.Length == 0 || PolicyHolder_DetailsInfo.Maiden_Surname.Length == 0 ||
                PolicyHolder_DetailsInfo.Surname.Length == 0 || PolicyHolder_DetailsInfo.ID_number.Length == 0 || PolicyHolder_DetailsInfo.Ethnicity.Length == 0 ||
                PolicyHolder_DetailsInfo.Maiden_Surname.Length == 0 || PolicyHolder_DetailsInfo.CellPhone_number.Length == 0 || PolicyHolder_DetailsInfo.Email.Length == 0||
                PolicyHolder_DetailsInfo.Nationality.Length == 0 || PolicyHolder_DetailsInfo.Country_Of_Birth.Length == 0 || PolicyHolder_DetailsInfo.Country_Of_Residence.Length == 0||
                PolicyHolder_DetailsInfo.Gross.Length == 0 || PolicyHolder_DetailsInfo.Permanent.Length == 0 || PolicyHolder_DetailsInfo.Salary_frequency.Length == 0 ||
                PolicyHolder_DetailsInfo.Gender.Length == 0 || PolicyHolder_DetailsInfo.DOB.Length == 0 || PolicyHolder_DetailsInfo.Card_Number.Length == 0||
                PolicyHolder_DetailsInfo.Covered.Length == 0 || PolicyHolder_DetailsInfo.Cover_Amount.Length == 0)
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
                    String sql = "UPDATE PolicyHolder_Details " +
                                 "SET Scenario_ID=@Scenario_ID, Town=@Town, Worksite=@Worksite, Employment=@Employment, Maiden_Surname=@Maiden_Surname, ID_number=@ID_number, Ethnicity=@Ethnicity, Marital_Status=@Marital_Status, CellPhone_number=@CellPhone_number, Email=@Email, Nationality=@Nationality, Country_Of_Birth=@Country_Of_Birth, Country_Of_Residence=@Country_Of_Residence, Gross@Gross, Permanent=@Permanent, Salary_frequency=@Salary_frequency, Gender=@Gender, DOB=@DOB, Card_Number=@Card_Number, Covered=@Covered, Cover_Amount=@Cover_Amount " +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Scenario_ID", PolicyHolder_DetailsInfo.Scenario_ID);
                        command.Parameters.AddWithValue("@Town", PolicyHolder_DetailsInfo.Town);
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
                    PolicyHolder_DetailsInfo.Scenario_ID = ""; PolicyHolder_DetailsInfo.= ""; PolicyHolder_DetailsInfo. = "";
                    PolicyHolder_DetailsInfo. = ""; PolicyHolder_DetailsInfo. = ""; PolicyHolder_DetailsInfo. = "";
                    PolicyHolder_DetailsInfo. = ""; PolicyHolder_DetailsInfo. = ""; PolicyHolder_DetailsInfo. "";
                    PolicyHolder_DetailsInfo. = ""; PolicyHolder_DetailsInfo. = ""; PolicyHolder_DetailsInfo. = "";
                    PolicyHolder_DetailsInfo. = ""; PolicyHolder_DetailsInfo. = ""; PolicyHolder_DetailsInfo. = "";
                    PolicyHolder_DetailsInfo. = ""; PolicyHolder_DetailsInfo. = ""; PolicyHolder_DetailsInfo. = "";
                    PolicyHolder_DetailsInfo. = ""; PolicyHolder_DetailsInfo. = ""; PolicyHolder_DetailsInfo. = "";
                    PolicyHolder_DetailsInfo. = ""; PolicyHolder_DetailsInfo. = ""; PolicyHolder_DetailsInfo. = "";

                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

        }
    }

        public class PolicyHolder_DetailsInfo
        {
            public String id;
            public String Scenario_ID;
            public String Town;
            public String Worksite;
            public String Employment;
            public String First_name;
            public String Maiden_Surname;
            public String Surname;
            public String ID_number;
            public String Ethnicity;
            public String Marital_Status;
            public String CellPhone_number;
            public String Email;
            public String Nationality;
            public String Country_Of_Birth;
            public String Country_Of_Residence;
            public String Gross;
            public String Permanent;
            public String Salary_frequency;
            public String Gender;
            public String DOB;
            public String Card_Number;
            public String Covered;
            public String Cover_Amount;


        }
    }
