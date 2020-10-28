
using LMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositary
{
   public class EmployeeRepository
    {
        public Employee save(Employee obj)
        {
            EmployeeDbContext db = new EmployeeDbContext();
            Employee test = db.Employee.Where(temp => temp.Employeeid == obj.Employeeid).FirstOrDefault();
            test.Departmentid = obj.Departmentid;
            test.Mobile = obj.Mobile;
            test.SpecialPermission = obj.SpecialPermission;
            test.EmployeeDesignation = obj.EmployeeDesignation;
            db.SaveChanges();
            return obj;


        }

        public List<Employee> GetEmployees()
        {
            EmployeeDbContext db = new EmployeeDbContext();
            List<Employee> emps = db.Employee.ToList();
            return emps;
        }

        public Employee Register(Employee obj)
        {
            using (EmployeeDbContext db = new EmployeeDbContext())
            {
                Employee test = new Employee();
                test.EmployeeName = obj.EmployeeName;
                test.Email = obj.Email;
                test.Password = obj.Password;
                test.EmployeeDesignation =obj.EmployeeDesignation;
                test.Mobile =obj.Mobile;
                test.SpecialPermission =obj.SpecialPermission;
                test.Employeeid = obj.Employeeid;
                test.Departmentid =obj.Departmentid;

                db.Employee.Add(test); db.SaveChanges();
                return test;
            }
        }
    }
}
