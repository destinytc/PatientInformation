using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;
namespace PatientInformation.Pages.Admin
{
    public class EditAdminModel : PageModel
    {

        public AdminInfo adminInfo = new AdminInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["admin_id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=PatientInfo;Integrated Security=True;Encrypt=False;Trust Server Certificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "SELECT * FROM admin WHERE admin_id = @admin_id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                       
                        command.Parameters.AddWithValue("@admin_id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                adminInfo.admin_id = "" + reader.GetInt32(0);
                                adminInfo.admin_firstName = reader.GetString(1);
                                adminInfo.admin_lastName = reader.GetString(2);
                                adminInfo.admin_email = reader.GetString(3);
                                adminInfo.admin_username = reader.GetString(4);
                                adminInfo.admin_password = reader.GetString(5);
                               


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
            adminInfo.admin_id = Request.Form["admin_id"];
            adminInfo.admin_firstName = Request.Form["admin_firstName"];
            adminInfo.admin_lastName = Request.Form["admin_lastName"];
            adminInfo.admin_email = Request.Form["admin_email"];
            adminInfo.admin_username = Request.Form["admin_username"];
            adminInfo.admin_password = Request.Form["admin_password"];
           

            if (adminInfo.admin_firstName.Length == 0 || adminInfo.admin_lastName.Length == 0 ||
              adminInfo.admin_email.Length == 0 || adminInfo.admin_username.Length == 0 ||
              adminInfo.admin_password.Length == 0 )
            {
                @errorMessage = "Please fill all required fields!";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=PatientInfo;Integrated Security=True;Encrypt=False;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE admin " +
                        "SET admin_firstName= @admin_firstName, admin_lastName= @admin_lastName, admin_email=@admin_email, admin_username=@admin_username, admin_password=@admin_password " +
                        "WHERE admin_id= @admin_id;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@admin_id", adminInfo.admin_id);
                        command.Parameters.AddWithValue("@admin_firstName", adminInfo.admin_firstName);
                        command.Parameters.AddWithValue("@admin_lastName", adminInfo.admin_lastName);
                        command.Parameters.AddWithValue("@admin_email", adminInfo.admin_email);
                        command.Parameters.AddWithValue("@admin_username", adminInfo.admin_username);
                        command.Parameters.AddWithValue("@admin_password", adminInfo.admin_password);
                      

                        command.ExecuteNonQuery();




                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Admin/ViewAdmin");
        }
    }
}
