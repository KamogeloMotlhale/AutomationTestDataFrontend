using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.ComponentDowngradeUpgrade
{
    public class IndexModel : PageModel
    { 
    public List<DowngradeInfo> ListDowngradeComponent = new List<DowngradeInfo>();
    public void OnGet()
    {
        try
        {
            String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                String sql = "SELECT * FROM ComponentDowngradeUpgrade ";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                        while (reader.Read())
                        {

                            DowngradeInfo DowngradeInfo = new DowngradeInfo();
                            DowngradeInfo.id = reader["ScenarioID"].ToString();
                            DowngradeInfo.Method = reader["Method"].ToString();
                            DowngradeInfo.Cover_Amount = reader["Sum_assured"].ToString();
                            DowngradeInfo.Component = reader["Component"].ToString();
                            DowngradeInfo.comID = reader["component_IDNo"].ToString();
                            ListDowngradeComponent.Add(DowngradeInfo);
                        }


                }

            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception:" + ex.ToString());

        }
    }
}
public class DowngradeInfo
    {
        public String id;
        public String comID;
        public String Method;
        public String Cover_Amount;
        public String Component;
    
    }
}
