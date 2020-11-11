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
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;


namespace LeaveManagementSysytem.Controllers
{
    [MyExceptionFilter]
    public class AccountController : Controller
    {
        EmployeeBusiness obj;
        public AccountController()
        {
            obj = new EmployeeBusiness();

        }
        // GET: Account
        [ExceptEmployeeAuthorization]
        public ActionResult Employees()
        {
            List<Employee> EmployeeList = obj.GetAllEmployees();
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
                bool result = obj.RegisterEmployees(rvm);

                return RedirectToAction("Index", "Home");
            }

            else
                return View();
        }

        public ActionResult Delete(string Id)
        {
            Employee rvm = new Employee();
            rvm.Id = Id;
            Employee Emp = obj.DeleteEmployee(rvm);
            return RedirectToAction("Account", "Employees");
        }
        [HttpPost]
        public ActionResult SearchByRole(string SearchByRole)
        {
            
           List<Employee> Emp = obj.SearchRole(SearchByRole);
            return View(Emp);
        }
        public ActionResult Edit(string Id)
        {
            Employee rvm = new Employee();
            rvm.Id = Id;
            Employee Emp = obj.ViewEmployees(rvm);
            return View(Emp);
        }
        [HttpPost]
        public ActionResult Edit(Employee ob)
        {


            if (Request.Files.Count >= 1)
            {
                var file = Request.Files[0];
                var imgBytes = new byte[file.ContentLength];
                file.InputStream.Read(imgBytes, 0, file.ContentLength - 1);
                var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                ob.ImageUrl = base64String;
            }

            bool result = obj.EditEmployee(ob);


            return RedirectToAction("Account", "Employees");

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
                Session["CurrentUserName"] = user.EmployeeName;

                //login
                var authenticationManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);

                //if (userManager.IsInRole(user.Id, "Admin"))
                //{
                //    return RedirectToAction("Index", "Home", new { area = "Admin" });
                //}
                //else if (userManager.IsInRole(user.Id, "Manager"))
                //{
                //    return RedirectToAction("Index", "Home", new { area = "Manager" });
                //}
                //else
                //{
                //    return RedirectToAction("Index", "Home");
                //}

                string s= userManager.GetRoles(user.Id).FirstOrDefault();
                Session["Role"] = s;
                    Session["Employee"] = user;
                if ((user.EmployeeDesignation == "HR") && (user.SpecialPermission == true))
                {
                    Session["Special"] = "true";
                    
                }

                return RedirectToAction("Index", "Home");

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




        public ActionResult LeaveApply()
        {
            Employee emp = new Employee();
            emp.Id = User.Identity.GetUserId();
            Employee em = new Employee();
            em = obj.GetEmployeeById(emp.Id);
            Leave ob = new Leave();
            ob.Eid = em.Eid;
            // StartDate
            //EndDate
            //Number 
            ob.EmployeeName = em.EmployeeName;
            ob.DepartmentName = em.DepartmentName;
            ob.Status = "Pending";
            ob.Projectid = em.Projectid;

            return View(ob);
        }
        public ActionResult LeaveStatus()
        {
            Employee emp = new Employee();
            emp.Id = User.Identity.GetUserId();
            Employee em = new Employee();
            em = obj.GetEmployeeById(emp.Id);
            //Employee em = new Employee();
            //em = obj.GetEmployeeById(emp.Id);
            //Leave ob = new Leave();
            //ob.Eid = em.Eid;
            //// StartDate
            ////EndDate
            ////Number 
            //ob.EmployeeName = em.EmployeeName;
            //ob.DepartmentName = em.DepartmentName;
            //ob.Status = "Pending";
            //ob.Projectid = em.Projectid;
            List<Leave> LeaveList = obj.GetAllLeavesById(em.Eid);


            return View(LeaveList);
        }

        [HttpPost]
        public ActionResult LeaveApply(Leave ob)
        {

            obj.CreateLeave(ob);
            return RedirectToAction("Index", "Home");

        }


        [ExceptEmployeeAuthorization]
        public ActionResult LeaveView()
        {
            List<Leave> LeaveList = obj.GetAllLeaves();
            //Leave ob = new Leave();
            //ob.Eid = obj.Eid;
            //// StartDate
            ////EndDate
            ////Number 
            //ob.DepartmentName = obj.DepartmentName;
            //ob.Status = false;
            //ob.Projectid = obj.Projectid;
           
            return View(LeaveList);
        }


        [ManagerAndSpecialAuthorization]
        public ActionResult LeaveAction(int Id)
        {
            Leave ob = obj.GetLeaveById(Id);
            //Leave ob = new Leave();
            //ob.Eid = obj.Eid;
            //// StartDate
            ////EndDate
            ////Number 
            //ob.DepartmentName = obj.DepartmentName;
            //ob.Status = false;
            //ob.Projectid = obj.Projectid;
            return View(ob);
        }
        [ManagerAndSpecialAuthorization]
        [HttpPost]
        public ActionResult LeaveAction(Leave lv)
        {
            obj.UpdateLeave(lv);
            //Leave ob = new Leave();
            //ob.Eid = obj.Eid;
            //// StartDate
            ////EndDate
            ////Number 
            //ob.DepartmentName = obj.DepartmentName;
            //ob.Status = false;
            //ob.Projectid = obj.Projectid;
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Search()
        {
            List<Employee> EmployeeList = obj.GetAllEmployees();
            return View(EmployeeList);
        }
        [HttpPost]
        public ActionResult Search(string EmployeeName)
        {
            Employee ob = obj.FindEmployee(EmployeeName);
            return View("Search1",ob);
        }


        public JsonResult SendMailToUser()
        {
            bool result = false;

            result = SendEmail("praveeninpal@gmail.com", "Email sending test", "    <p> hai Praveen <br/>This email is for testing purpose <br/>regards </p>");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public bool SendEmail(string ToEmail, string subject, string emailBody)
        {
            try
            {
                string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword= System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                MailMessage mailMessage = new MailMessage(senderEmail, ToEmail, subject, emailBody);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(mailMessage);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

    }

}
