using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSysytem.ViewModels
{
    public class RegisterViewModel
    {
    public string Email { get; set; }
    public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password can't be blank")]
        [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
        public string ConfirmPassword { get; set; }
        public string EmployeeName { get; set; }
     public string Mobile { get; set; }
    public int Departmentid{ get; set; }
    public int Employeeid{ get; set; }
    public string EmployeeDesignation { get; set; }
        public int Projectid { get; set; }
    public bool SpecialPermission{  get; set; }
    }
}