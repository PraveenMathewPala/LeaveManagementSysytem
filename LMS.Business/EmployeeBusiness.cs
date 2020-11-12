using LMS.Model;
using Repositary;
using System;
using System.Collections.Generic;

namespace Business
{
    public class EmployeeBusiness
    {
        EmployeeRepository ob;
        LeaveRepository obj;
        public EmployeeBusiness()
        {
           ob = new EmployeeRepository();
           obj = new LeaveRepository();

        }
        public bool EditEmployee(Employee obj)
        {
           return ob.save(obj);
            
        }

        public List<Employee> GetAllEmployees()
        {
//Test Commit
           

            return ob.GetEmployees();
            
        }

        public bool RegisterEmployees(Employee obj)
        {
            return ob.Register(obj);
           
        }

        public Employee ViewEmployees(Employee rvm)
        {
            return ob.ViewEmp(rvm);
        }

        public void CreateLeave(Leave lv)
        {
            obj.Register(lv);

        }

        public List<Leave> GetAllLeaves()
        {
            return obj.GetLeaves();
        }

        public Leave GetLeaveById(int id)
        {
            return obj.GetLeave(id);

        }

        public void UpdateLeave(Leave lv)
        {
            obj.Update(lv);
        }

        public Employee FindEmployee(string empName)
        {
            return ob.FindByName(empName);
        }

        public Employee GetEmployeeById(string id)
        {
            return ob.GetById(id);
        }

        public Employee DeleteEmployee(Employee rvm)
        {
            return ob.Delete(rvm);
        }

        public List<Employee> SearchRole(string role)
        {
            return ob.SearchBy(role);
        }

        public List<Leave> GetAllLeavesById(int id)
        {
            return obj.SearchById(id);
        }

        public Employee LoginUser(Login lg)
        {
            return ob.Login(lg);
        }

       
    }
}
