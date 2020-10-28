using LMS.Model;
using Repositary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
   public class EmployeeBusiness
    {
       public Employee EditEmployee(Employee obj)
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

        public Employee RegisterEmployees(Employee obj)
        {
            EmployeeRepository ob = new EmployeeRepository();
            return ob.Register(obj);
           
        }
    }
}
