using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using PatientInformation.Pages.Patients;
using Microsoft.Data.SqlClient;

namespace PatientInformation.Pages.Admin
{
    public class ViewAdminModel : PageModel
    {
        public List<AdminInfo> listAdmin = new List<AdminInfo>();
        public void OnGet()
        {
            try
            {
                String adminConnectionString = "Data Source=.\\sqlexpress;Initial Catalog=PatientInfo;Integrated Security=True; Encrypt=False";
                
                using(SqlConnection connection = new SqlConnection(adminConnectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM admin";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AdminInfo adminInfo = new AdminInfo();
                                adminInfo.admin_id = "" + reader.GetInt32(0);
                                adminInfo.admin_firstName = reader.GetString(1);
                                adminInfo.admin_lastName = reader.GetString(2);
                                adminInfo.admin_email = reader.GetString(3);
                                adminInfo.admin_username = reader.GetString(4);
                                adminInfo.admin_password = reader.GetString(5);
                               

                                listAdmin.Add(adminInfo);
                            }
                        }
                    }

                }
            
            
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
    public class AdminInfo
    {
        public String admin_id;
        public String admin_firstName;
        public String admin_lastName;
        public String admin_email;
        public String admin_username;
        public String admin_password;


    }
}
