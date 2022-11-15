using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.CollectionMethodData
{
    public class CreateModel : PageModel
    {
        public ChangeCollectionMethodInfo ChangeCollectionMethodInfo = new ChangeCollectionMethodInfo(); 
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            ChangeCollectionMethodInfo.id = Request.Query["scenarioid"];
        }

        public void OnPost()
        {
            ChangeCollectionMethodInfo.id = Request.Query["scenarioid"];
            ChangeCollectionMethodInfo.employee_number1 = Request.Form["employee_number1"];
            ChangeCollectionMethodInfo.collectionmethod = Request.Form["collectionmethod"];

            if (ChangeCollectionMethodInfo.employee_number1.Length == 0 || ChangeCollectionMethodInfo.collectionmethod.Length == 0)
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
                    String sql = "INSERT INTO CollectionMethodData " +
                                "(employee_number1,  collectionmethod) VALUES" +
                                "(@employee_number1,  @collectionmethod) WHERE Scenario_ID=@id;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@employee_number1", ChangeCollectionMethodInfo.employee_number1);
                        command.Parameters.AddWithValue("@collectionmethod", ChangeCollectionMethodInfo.collectionmethod);


                        ;

                        command.ExecuteNonQuery();

                    }

                }
                ChangeCollectionMethodInfo.employee_number1 = "";  ChangeCollectionMethodInfo.collectionmethod = "";
                successMessage = "New Change Collection Method Data  Added Successfully";

            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

               Response.Redirect("/PolicyServicing/CollectionMethodData");
           
        }
    }
}
