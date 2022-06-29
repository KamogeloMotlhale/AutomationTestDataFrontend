using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.Upgrade
{


    public class IndexModel : PageModel
    {
        public List<UpgradeInfo> ListUpgradeComponent = new List<UpgradeInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    String sql = "SELECT * FROM Upgrade ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                            while (reader.Read())
                            {

                                UpgradeInfo UpgradeInfo = new UpgradeInfo();
                                UpgradeInfo.id = "" + reader.GetInt32(0);
                                UpgradeInfo.Date = reader.GetString(1);
                                UpgradeInfo.Component = reader.GetString(2);
                                UpgradeInfo.Cover_Amount = reader.GetString(3);



                                ListUpgradeComponent.Add(UpgradeInfo);
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

        public class UpgradeInfo
        {
            public String id;
            public String Date;
            public String Component;
            public String Cover_Amount;


        }
    }

