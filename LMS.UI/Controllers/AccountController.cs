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
        Employee user;

        public AccountController()
        {
            obj = new EmployeeBusiness();
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


        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        public ActionResult Login(Login lg)
        {
            user = obj.LoginUser(lg);
            if (user != null)
            {
                Session["CurrentUser"] = user.UserName;

                Session["CurrentUserName"] = user.EmployeeName;
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
            var id = User.Identity.GetUserId();
            Employee appUser = obj.GetEmployeeById(id);
            return View(appUser);
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
            if(result)
                return RedirectToAction("MyProfile", "Account");
            else
                return RedirectToAction("Error");

        }


        public ActionResult Delete(string Id)
        {
            Employee rvm = new Employee();
            rvm.Id = Id;
            Employee Emp = obj.DeleteEmployee(rvm);
            return RedirectToAction("Account", "Employees");
        }


        // GET: Account
        [ExceptEmployeeAuthorization]
        public ActionResult Employees()
        {
            List<Employee> EmployeeList = obj.GetAllEmployees();
            return View(EmployeeList);
        }
       

        [HttpPost]
        public ActionResult SearchByRole(string SearchByRole)
        {
            
           List<Employee> Emp = obj.SearchRole(SearchByRole);
            return View(Emp);
        }


        
        public ActionResult LeaveApply()
        {
            Leave ob = new Leave();
            ob.Eid = user.Eid;            
            ob.EmployeeName = user.EmployeeName;
            ob.DepartmentName = user.DepartmentName;
            ob.Status = "Pending";
            ob.Projectid = user.Projectid;
            return View(ob);
        }

        [HttpPost]
        public ActionResult LeaveApply(Leave ob)
        {
            obj.CreateLeave(ob);
            return RedirectToAction("Index", "Home");
        }


        public ActionResult LeaveStatus()
        {
            List<Leave> LeaveList = obj.GetAllLeavesById(user.Eid);
            return View(LeaveList);
        }


        [ExceptEmployeeAuthorization]
        public ActionResult LeaveView()
        {
            List<Leave> LeaveList = obj.GetAllLeaves();
            return View(LeaveList);
        }


        [ManagerAndSpecialAuthorization]
        public ActionResult LeaveAction(int Id)
        {
            Leave ob = obj.GetLeaveById(Id);
            return View(ob);
        }

        [ManagerAndSpecialAuthorization]
        [HttpPost]
        public ActionResult LeaveAction(Leave lv)
        {
            obj.UpdateLeave(lv);
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
