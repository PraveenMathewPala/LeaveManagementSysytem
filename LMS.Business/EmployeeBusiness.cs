using LMS.Model;
using Repositary;
using System;
using System.Collections.Generic;

namespace Business
{
    public class EmployeeBusiness
    {
       public bool EditEmployee(Employee obj)
        {
            EmployeeRepository ob = new EmployeeRepository();
           return ob.save(obj);
            
        }

        public List<Employee> GetAllEmployees()
        {
            EmployeeRepository Db = new EmployeeRepository();
//Test Commit
           

            return Db.GetEmployees();
            
        }

        public bool RegisterEmployees(Employee obj)
        {
            EmployeeRepository ob = new EmployeeRepository();
            return ob.Register(obj);
           
        }

        public Employee ViewEmployees(Employee rvm)
        {
            EmployeeRepository ob = new EmployeeRepository();
            return ob.ViewEmp(rvm);
        }

       

    }
}
