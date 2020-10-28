using LMS.Model;
using Repositary;
using System.Collections.Generic;

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

           

            return Db.GetEmployees();
            
        }

        public Employee RegisterEmployees(Employee obj)
        {
            EmployeeRepository ob = new EmployeeRepository();
            return ob.Register(obj);
           
        }
    }
}
