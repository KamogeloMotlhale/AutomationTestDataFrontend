using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyHolder_Details
{
    public class IndexModel : PageModel
    {
        public List<PolicyHolder_DetailsInfo> ListPolicyHolder_Details = new List<PolicyHolder_DetailsInfo>();
        public void OnGet()
        {
            try
            { 
              String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection (connectionString))
                {

                    connection.Open();
                    String sql = "SELECT * FROM PolicyHolder_Details";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader()) 
                        while(reader.Read())
                         {

                                PolicyHolder_DetailsInfo PolicyHolder_DetailsInfo = new PolicyHolder_DetailsInfo();
                                PolicyHolder_DetailsInfo.id =""+ reader.GetInt32(0);
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

                                ListPolicyHolder_Details.Add(PolicyHolder_DetailsInfo);
                         }


                    }

                }
            
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());

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
