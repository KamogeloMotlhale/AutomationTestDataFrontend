using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.Upgrade
{
    public class CreateModel : PageModel
    {
        public UpgradeInfo UpgradeInfo = new UpgradeInfo(); 
        public String errorMessage = "";
        public String successMessage = "";

        public void validateForm()
        {

            if (UpgradeInfo.Date.Length == 0 || UpgradeInfo.Component.Length == 0 || UpgradeInfo.Cover_Amount.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;




            }
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            UpgradeInfo.Date = Request.Form["Date"];
            UpgradeInfo.Component = Request.Form["Component"];
            UpgradeInfo.Cover_Amount = Request.Form["Cover_Amount"];

            validateForm();
           

            //save new Life assured to DB
            try 
            {
               
                String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();
                    String sql = "INSERT INTO UpGrade " +
                                "(Date, Component, Cover_Amount) VALUES" +
                                "(@Date, @Component, @Cover_Amount);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Date", UpgradeInfo.Date);
                        command.Parameters.AddWithValue("@Component", UpgradeInfo.Component);
                        command.Parameters.AddWithValue("@Cover_Amount", UpgradeInfo.Cover_Amount);
                        ;

                        command.ExecuteNonQuery();

                    }


                }
                UpgradeInfo.Date = ""; UpgradeInfo.Component = ""; UpgradeInfo.Cover_Amount = "";
                successMessage = "New Upgrade Component Added Successfully";
                return;

            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

        
            //Response.Redirect("/PolicyServicing/Upgrade");
        }
    }
}
public class UpgradeInfo
{
    public String id;
    public String Date;
    public String Component;
    public String Cover_Amount;

}

