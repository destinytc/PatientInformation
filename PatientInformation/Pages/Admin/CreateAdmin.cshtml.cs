using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using PatientInformation.Pages.Patients;

namespace PatientInformation.Pages.Admin
{
    public class CreateAdminModel : PageModel
    {
        public AdminInfo adminInfo = new AdminInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
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
                String adminconnectionString = "Data Source=.\\sqlexpress;Initial Catalog=PatientInfo;Integrated Security=True;Encrypt=False;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(adminconnectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO admin " +
                        "(admin_firstName,admin_lastName, admin_email, admin_username,admin_password) VALUES" +
                        "(@admin_firstName, @admin_lastName, @admin_email, @admin_username, @admin_password);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
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

            adminInfo.admin_firstName = "";
            adminInfo.admin_lastName = "";
            adminInfo.admin_email = "";
            adminInfo.admin_username= "";
            adminInfo.admin_password = "";
          ;

            successMessage = "Admin Added!";

            Response.Redirect("/Admin/ViewAdmin");
        }
    
    }
}
