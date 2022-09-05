using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.NewBusiness
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public String errorMessage = "";
        public String successMessage = "";

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        //public void SetUserID(string value)
        //{
        //    CookieOptions option = new CookieOptions();
        //    option.Expires = DateTime.Now.AddMinutes(60);
        //    Response.Cookies.Append("UserID", value, option);
        //}



        //public string GetUserID()
        //{
        //    string userId = Request.Cookies["UserID"];
        //    return userId;
        //}
        public void OnGet()
        {


        }
        public void OnPost()
        {
            string fileup = Request.Form["file"];

            if (fileup.Length == 0)
            {

                errorMessage = "Please select a file";
                return;
            }
            try
            {
                var sqlConnStr = "Data Source=PF30FYP5;Integrated Security=True";
                string con = @"Provider= Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\e697642\Documents\GitHub\AutomationTestDataFrontend\Automation Test Data App\TestData.xlsx;" + @"Extended Properties='Excel 8.0;HDR=No;'";
                string tableName = "AddaLife";
                int colNum = 51;
                //Row iteration
                using (OleDbConnection connection = new OleDbConnection(con))
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand("select * from [" + tableName + "$]", connection);
                    using (OleDbDataReader dr = command.ExecuteReader())
                    {
                        int rwCount = 0;
                        String headers = " ";
                        String headers1 = " ";
                        while (dr.Read())
                        {
                            String values = "";
                            //Column iterator
                            for (int i = 0; i < colNum; i++)
                            {
                                if (rwCount == 0)
                                {
                                    headers = headers + dr[i].ToString() + " varchar(255)";
                                    headers1 = headers1 + dr[i].ToString();
                                    if (i != (colNum - 1))
                                    {
                                        headers = headers + ",";
                                        headers1 = headers1 + ",";
                                    }
                                }
                                else
                                {
                                    values = values + "'" + dr[i].ToString() + "'";
                                    if (i != (colNum - 1))
                                    {
                                        values = values + ",";
                                    }
                                }
                            }
                            if (rwCount == 0)
                            {
                                using (SqlConnection connection1 = new SqlConnection(sqlConnStr))
                                {
                                    connection1.Open();
                                    String sql = "CREATE TABLE " + tableName + " (" + headers + ");";
                                    using (SqlCommand cmmd = new SqlCommand(sql, connection1))
                                    {
                                        cmmd.ExecuteNonQuery();
                                    }
                                }
                            }
                            else
                            {
                                //Using SQL
                                using (SqlConnection connection1 = new SqlConnection(sqlConnStr))
                                {
                                    connection1.Open();
                                    String sql = "INSERT INTO SafricanJustFuneral(" + headers1 + ")";
                                    sql = sql + "VALUES(" + values + ");";
                                    using (SqlCommand cmmd = new SqlCommand(sql, connection1))
                                    {
                                        cmmd.ExecuteNonQuery();
                                    }
                                }
                            }
                            rwCount++;
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

