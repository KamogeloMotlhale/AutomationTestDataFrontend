using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.AffordabilityCheckInfo
{
    public class IndexModel : PageModel
    {
    }


    public class PolicyHolderInfo
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
        public String Country_Of_Residence;
        public String Gross;
        public String Permanent;
        public String Salary_frequency;
        public String Covered;
        public String Cover_Amount;
       


    }
}
