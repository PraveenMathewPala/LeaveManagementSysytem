
using LMS.Model;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;

namespace Repositary
{
   public class EmployeeRepository
    {
        EmployeeDbContext appDbContext ;
        ApplicationUserStore appUserStore;
        ApplicationUserManager userManager ;

        public EmployeeRepository()
        {
            appDbContext = new EmployeeDbContext();
           appUserStore = new ApplicationUserStore(appDbContext);
            userManager = new ApplicationUserManager(appUserStore);


        }

        public bool save(Employee obj)
        {

            var test = userManager.FindById(obj.Id);
                //Users.Where(temp => temp.Eid == obj.Eid).FirstOrDefault();

            test.DepartmentName = obj.DepartmentName;
            test.Mobile = obj.Mobile;
            test.SpecialPermission = obj.SpecialPermission;
            test.EmployeeDesignation = obj.EmployeeDesignation;
            test.ImageUrl = obj.ImageUrl;
            var result = userManager.Update(test);
            if (result.Succeeded)
            {
                

                userManager.RemoveFromRole(test.Id, userManager.GetRoles(test.Id).FirstOrDefault());
                //userManager.RemoveFromRole(userManager.GetR);

                if (obj.DepartmentName == LMSConstants.role_hr)
                    userManager.AddToRole(test.Id, "Admin");
                if (obj.DepartmentName == "HR" && (obj.SpecialPermission == true || obj.EmployeeDesignation == "ProjectManager"))
                    userManager.AddToRole(test.Id, "AdminSpecial");
                else if (obj.EmployeeDesignation == "ProjectManager")
                    userManager.AddToRole(test.Id, "Manager");
                else
                    userManager.AddToRole(test.Id, "Customer");


            }
            return result.Succeeded;


        }

        public Leave GetbyName(string v)
        {
            Leave ob = new Leave();
            Employee user= userManager.Users.Where(temp => temp.UserName == v).FirstOrDefault();
            ob.Eid = user.Eid;
            ob.EmployeeName = user.EmployeeName;
            ob.DepartmentName = user.DepartmentName;
            ob.Status = "Pending";
            ob.Projectid = user.Projectid;
            return ob;
        }

        public Employee Login(Login lg)
        {
            Employee user= userManager.Users.Where(temp => (temp.Email == lg.Username && temp.Password == lg.Password)).FirstOrDefault();

            if (user != null)
            {
                

                //login
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);

               }
                return user;
          
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> emps = userManager.Users.Select(temp => temp).ToList();
            return emps;
        }

        public Employee FindByName(string empName)
        {
            return userManager.Users.Where(temp => temp.EmployeeName == empName).FirstOrDefault();
        }

        public Employee Delete(Employee rvm)
        {
            Employee ev =userManager.Users.Where(temp => temp.Id == rvm.Id).FirstOrDefault();

            userManager.Delete(GetById(rvm.Id));
            //Where(temp => temp.Id == rvm.Id);
            return ev;
        }

       
        public List<Employee> SearchBy(string Role)
        {
            return userManager.Users.Where(temp => temp.EmployeeDesignation==Role).ToList();
        }

        public Employee GetById(string id)
        {
           
            Employee emp= userManager.Users.Where(temp => temp.Id == id).FirstOrDefault();
            return emp;
        }

        public Employee ViewEmp(Employee rvm)
        {

            Employee test = userManager.FindById(rvm.Id);
            return test;
        }

       public bool Register(Employee obj)
        {
            obj.UserName = obj.Email;
            var result = userManager.Create(obj);
            if(result.Succeeded)
            {

               
                if (obj.DepartmentName == "HR" && (obj.SpecialPermission == true || obj.EmployeeDesignation == "ProjectManager"))
                    userManager.AddToRole(obj.Id, "AdminSpecial");
                else if (obj.DepartmentName == LMSConstants.role_hr)
                    userManager.AddToRole(obj.Id, "Admin");
                else if (obj.EmployeeDesignation == "ProjectManager")
                    userManager.AddToRole(obj.Id, "Manager");
                else
                    userManager.AddToRole(obj.Id, "Customer");

            }
            return result.Succeeded ; 

            
        }

       
    }
}
