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
            String connectionString = "Data Source=(LocalDB)/MSSQLLocalDB;AttachDbFilename=C:/Users/G992107/Documents/Github/ilrsafricanautopolicyservicing/data/Automation.mdf;Integrated Security=True;Connect Timeout=30";
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
                            DowngradeInfo.id = "" + reader.GetInt32(0);
                            DowngradeInfo.Date = reader.GetString(1);
                            DowngradeInfo.Method = reader.GetString(2);
                            DowngradeInfo.Cover_Amount = reader.GetString(3);



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
        public String Date;
        public String Method;
        public String Cover_Amount;
  

    }
}
