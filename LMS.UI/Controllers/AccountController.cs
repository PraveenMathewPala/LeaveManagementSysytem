using Business;
using LMS.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LeaveManagementSysytem.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            EmployeeBusiness obj = new EmployeeBusiness();
            List<Employee> EmployeeList=obj.GetAllEmployees();
            return View(EmployeeList);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Employee rvm)
        {
            if (ModelState.IsValid)
            {

                EmployeeBusiness obj = new EmployeeBusiness();
                Employee Emp = obj.RegisterEmployees(rvm);
               
                return RedirectToAction("Index", "Home");
            }

            else
                return View();
        }

        public ActionResult Edit(int Eid)
        {
           
           // EmployeeDbContext db = new EmployeeDbContext();
            //Employee test = db.Employee.Where(temp => temp.Employeeid == Eid).FirstOrDefault();

            return View();
        }
        [HttpPost]
        public ActionResult Edit(Employee obj)
        {

            EmployeeBusiness ob = new EmployeeBusiness();
           Employee test= ob.EditEmployee(obj);
            
            return View(test);
          //  return RedirectToAction("Index", "Home");

        }
    }
}