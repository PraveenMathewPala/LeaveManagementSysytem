using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using Microsoft.Owin;
using Owin;


namespace LeaveManagementSysytem.Filter
{
    public class MyAuthenticationFilter : FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated == false)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
        }
        //public void Configuration(IAppBuilder app)
        //{
        //    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        //}
    }
}
