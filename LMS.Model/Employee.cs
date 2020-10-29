using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Model
{
    public class Employee
    {
        // private LeaveManagementSysytem db = new LeaveManagementSysytem();


        public string Email { get; set; }


        public string Password { get; set; }


        public string EmployeeName { get; set; }


        public string Mobile { get; set; }

        public int Departmentid
        {
            get; set;
        }

        public int Employeeid
        {
            get; set;
        }
        public string EmployeeDesignation { get; set; }


        public bool SpecialPermission
        {
            get; set;
        }

        public string ImageUrl { get; set; }

    }
}
