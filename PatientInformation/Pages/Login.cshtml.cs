using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PatientInformation.Pages.Admin;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;


namespace PatientInformation.Pages
{
    public class LoginModel : PageModel
    {
        public List<AdminInfo> listAdmin = new List<AdminInfo>();
        public String admin_username;
        public String admin_password;
        public String errorMessage = "";
        public int count;

       public void OnGet()
        {
          
        }
        public void OnPost()
        {
            
            SqlConnection conn = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=PatientInfo;Integrated Security=True; Encrypt=False; Trust Server Certificate=True");
            conn.Open();
            string query = "SELECT COUNT(*) from admin WHERE admin_username=@admin_username AND admin_password=@admin_password;";

            SqlCommand cmd = new SqlCommand(query, conn);
            
            admin_username = Request.Form["admin_username"];
            admin_password = Request.Form["admin_password"];

            cmd.Parameters.AddWithValue("@admin_username", admin_username);
            cmd.Parameters.AddWithValue("@admin_password", admin_password);

            int count = (int)cmd.ExecuteScalar();
            conn.Close();

            if (count > 0)
            {
                
                Response.Redirect("/Admin/ViewAdmin");
            }
            else
            {
                errorMessage = "Username or Password is incorrect!";
                
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
