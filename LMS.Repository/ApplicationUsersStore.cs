using LMS.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositary
{
   
        public class ApplicationUserStore : UserStore<Employee>
        {
            public ApplicationUserStore(EmployeeDbContext dbContext) : base(dbContext)
            {

            }
        }
    }


