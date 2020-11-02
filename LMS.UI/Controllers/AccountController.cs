using Business;
using LeaveManagementSysytem.Filter;
using LeaveManagementSysytem.Identity;
using LMS.Model;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Repositary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace LeaveManagementSysytem.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [MyAuthenticationFilter]
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
                //rvm.PasswordHash = Crypto.HashPassword(rvm.Password);
                EmployeeBusiness obj = new EmployeeBusiness();
                bool result = obj.RegisterEmployees(rvm);
               
                return RedirectToAction("Index", "Home");
            }

            else
                return View();
        }

        public ActionResult Edit(string Id)
        {
            Employee rvm = new Employee();
            rvm.Id = Id;
            EmployeeBusiness obj = new EmployeeBusiness();
            Employee Emp = obj.ViewEmployees(rvm);


            return View(Emp);
        }
        [HttpPost]
        public ActionResult Edit(Employee obj)
        {

            EmployeeBusiness ob = new EmployeeBusiness();

            if(Request.Files.Count>=1)
            {
                var file = Request.Files[0];
                var imgBytes = new byte[file.ContentLength - 1];
                file.InputStream.Read(imgBytes, 0, file.ContentLength-1);
                var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                obj.ImageUrl = base64String;
            }

           bool result= ob.EditEmployee(obj);
            
            
          return RedirectToAction("Index", "Home");

        }

        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        public ActionResult Login(Login lg)
        {


            //var ps = Crypto.HashPassword(lg.Password); 
            //login
            var appDbContext = new EmployeeDbContext();
            var userStore = new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(userStore);
            var user = userManager.Users.Where(temp => (temp.Email == lg.Username && temp.Password == lg.Password)).FirstOrDefault();
            if (user != null)
            {
                //login
                var authenticationManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);

                if (userManager.IsInRole(user.Id, "Admin"))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else if (userManager.IsInRole(user.Id, "Manager"))
                {
                    return RedirectToAction("Index", "Home", new { area = "Manager" });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("myerror", "Invalid username or password");
                return View();
            }
        }

        // GET: Account/Logout
        public ActionResult Logout()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/MyProfile
        public ActionResult MyProfile()
        {
            var appDbContext = new EmployeeDbContext();
            var userStore = new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(userStore);
            Employee appUser = userManager.FindById(User.Identity.GetUserId());
                //FindById(User.Identity.GetUserId());
            return View(appUser);
        }
    }
}
