using Automation_Test_Data_App.Pages.PolicyServicing.CollectionMethodData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.CollectionMethodData
{
    public class EditModel : PageModel
    { 
    public ChangeCollectionMethodInfo ChangeCollectionMethodInfo = new ChangeCollectionMethodInfo();
    public String errorMessage = "";
    public String successMessage = "";

        public void OnGet()
        {

            String id = Request.Query["id"];

            try
            {

                string userId = Request.Cookies["UserID"];
                if (userId == null)
                {
                    Response.Redirect("/");
                }
                

                String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    String sql = "SELECT * FROM CollectionMethodData WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {

                                ChangeCollectionMethodInfo.id = String.Empty + reader["Scenario_ID"].ToString();
                                ChangeCollectionMethodInfo.employee_number1 = String.Empty + reader["employee_number1"].ToString();
                                ChangeCollectionMethodInfo.collectionmethod = String.Empty + reader["collectionmethod"].ToString();

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
            ChangeCollectionMethodInfo.id = Request.Form["id"];
            ChangeCollectionMethodInfo.employee_number1 = Request.Form["employee_number1"];
            ChangeCollectionMethodInfo.collectionmethod = Request.Form["collectionmethod"];

            if (ChangeCollectionMethodInfo.employee_number1.Length == 0 ||  ChangeCollectionMethodInfo.collectionmethod.Length == 0)
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
                    String sql = "UPDATE CollectionMethodData " +
                                 "SET employee_number1=@employee_number1,  collectionmethod=@collectionmethod " +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@employee_number1", ChangeCollectionMethodInfo.employee_number1);
                        command.Parameters.AddWithValue("@collectionmethod", ChangeCollectionMethodInfo.collectionmethod);

                        command.Parameters.AddWithValue("@id", ChangeCollectionMethodInfo.id);



                        command.ExecuteNonQuery();

                    }
                     ChangeCollectionMethodInfo.employee_number1 = ""; ChangeCollectionMethodInfo.collectionmethod = "";
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
