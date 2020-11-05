using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using Microsoft.Owin.Security.Cookies;
using Repositary;
using LMS.Model;

[assembly: OwinStartup(typeof(LeaveManagementSysytem.Startup1))]

namespace LeaveManagementSysytem
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions() { AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie, LoginPath = new PathString("/Account/Login") });
            this.CreateRolesAndUsers();
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }

        public void CreateRolesAndUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new EmployeeDbContext()));
            var appDbContext = new EmployeeDbContext();
            var appUserStore = new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(appUserStore);

            //Create Admin Role
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            //Create Admin User
            if (userManager.FindByName("admin") == null)
            {
                
                var user = new Employee();
                user.EmployeeName = "AdminFirst";
                user.Mobile = "9876543210";
                user.EmployeeDesignation = "Admin";
                user.DepartmentName = "Admin";
                user.UserName = "admin";
                user.Email = "admin@gmail.com";
                string Password = "admin123";
                user.Password="admin123";
                var chkUser = userManager.Create(user, Password);
                if (chkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }

            ////Create Manager Role
            //if (!roleManager.RoleExists("Manager"))
            //{
            //    var role = new IdentityRole();
            //    role.Name = "Manager";
            //    roleManager.Create(role);
            //}

            ////Create Manager User
            //if (userManager.FindByName("manager") == null)
            //{
            //    var user = new Employee();
            //    user.EmployeeName = "ManagerFirst";
            //    user.Mobile = "9876543210";
            //    user.EmployeeDesignation = "Manager";
            //    user.DepartmentName = "Admin";
            //    user.UserName = "manager";
            //    user.Email = "manager@gmail.com";
            //    user.Password = "manager123";
            //    var chkUser = userManager.Create(user);
            //    if (chkUser.Succeeded)
            //    {
            //        userManager.AddToRole(user.Id, "Manager");
            //    }
            //}

            ////Create Customer Role
            //if (!roleManager.RoleExists("Customer"))
            //{
            //    var role = new IdentityRole();
            //    role.Name = "Customer";
            //    roleManager.Create(role);
            //}

        }



    }
}
