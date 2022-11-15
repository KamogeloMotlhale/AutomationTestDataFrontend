using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.CollectionMethodData
{
    public class IndexModel : PageModel
    {
        public List<ChangeCollectionMethodInfo> ListChangeCollectionMethod = new List<ChangeCollectionMethodInfo>();
        public void OnGet()
        {
            try
            { 
              String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection (connectionString))
                {

                    connection.Open();
                    String sql = "SELECT * FROM CollectionMethodData ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader()) 
                        while(reader.Read())
                         {

                                ChangeCollectionMethodInfo ChangeCollectionMethodInfo = new ChangeCollectionMethodInfo();
                                ChangeCollectionMethodInfo.id = String.Empty + reader["Scenario_ID"].ToString(); 
                                ChangeCollectionMethodInfo.employee_number1 = String.Empty + reader["employee_number1"].ToString();
                                ChangeCollectionMethodInfo.collectionmethod = String.Empty + reader["collectionmethod"].ToString();
                                ListChangeCollectionMethod.Add(ChangeCollectionMethodInfo);
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

    public class ChangeCollectionMethodInfo
    {
        public String id;
        public String employee_number1;
        public String collectionmethod;


    }
}
