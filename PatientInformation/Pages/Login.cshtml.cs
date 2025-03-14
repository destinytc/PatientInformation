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
       
        public void OnGet()
        {
           

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
