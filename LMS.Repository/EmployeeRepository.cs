
using LMS.Model;
using Microsoft.AspNet.Identity;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
            return result.Succeeded;


        }

       

        public List<Employee> GetEmployees()
        {
            List<Employee> emps = userManager.Users.Select(temp => temp).ToList();
            return emps;
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
            return result.Succeeded ; 
            
        }

       
    }
}
