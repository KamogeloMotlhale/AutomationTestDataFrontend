using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.Test
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            try
            {
                var sqlConnStr = "Data Source=PF30FYP5;Integrated Security=True";
                string con = @"Provider= Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\G992127\Documents\GitHub\AutomationTestDataFrontend\Automation Test Data App\Automation Test Data App\ProvidProtect.xlsx;" + @"Extended Properties='Excel 8.0;HDR=Yes;'";
                //Row iteration

                using (OleDbConnection connection = new OleDbConnection(con))
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand("select * from [Provide_and_Protect$]", connection);
                    using (OleDbDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            //Column iterator
                            String values = "";
                            for (int i = 0; i < 28; i++)
                            {

                                values = values + "'" + dr[i].ToString() + "'";
                                if(i != 27)
                                {
                                    values = values+",";
                                }
                            }
                            //Using SQL
                            using(SqlConnection connection1 = new SqlConnection(sqlConnStr))
                            {
                                connection1.Open();
                                String sql = "INSERT INTO ProvideProtect (Age,Policy_Fee,LifeCover_RateGroup1_Male,"+
                                    "LifeCover_RateGroup2_Male,LifeCover_RateGroup3_Male,LifeCover_RateGroup4_Male,LifeCover_RateGroup1_Female,"
                                    +"LifeCover_RateGroup2_Female,LifeCover_RateGroup3_Female,LifeCover_RateGroup4_Female,PIMain_OccClassA_Male,"
                                    +"PIMain_OccClassB_Male,PIMain_OccClassC_Male,PIMain_OccClassD_Male,PIMain_OccClassE_Male,PIMain_OccClassA_Female,"
                                    +"PIMain_OccClassB_Female,PIMain_OccClassC_Female,PIMain_OccClassD_Female,PIMain_OccClassE_Female,DreadDiseasePLA_RateGroup1_Male,"
                                    +"DreadDiseasePLA_RateGroup2_Male,DreadDiseasePLA_RateGroup3_Male,DreadDiseasePLA_RateGroup4_Male,DreadDiseasePLA_RateGroup1_Female,"
                                    +"DreadDiseasePLA_RateGroup2_Female,DreadDiseasePLA_RateGroup3_Female,DreadDiseasePLA_RateGroup4_Female)";
                                sql = sql + "VALUES(" + values + ");";
                                using(SqlCommand cmmd = new SqlCommand(sql, connection1))
                                {
                                    cmmd.ExecuteNonQuery();
                                }

                            }
                            



                        }
                    }
                }


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
