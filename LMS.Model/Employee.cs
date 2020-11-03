using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Model
{
    public class Employee : IdentityUser
    {
        // private LeaveManagementSysytem db = new LeaveManagementSysytem();

       
        public string EmployeeName { get; set; }


        public string Mobile { get; set; }

        public string DepartmentName
        {
            get; set;
        }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string EmployeeDesignation { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Eid { get; set; }
        public bool SpecialPermission
        {
            get; set;
        }

        public string ImageUrl { get; set; }

        public int Projectid { get; set; }

    }
}
