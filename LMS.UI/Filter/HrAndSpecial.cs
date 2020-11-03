using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaveManagementSysytem.Filter
{
    public class HrAndSpecial : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.IsInRole("Admin") == false && filterContext.HttpContext.User.IsInRole("AdminSpecial") == false)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}