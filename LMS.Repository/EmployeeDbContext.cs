using LMS.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositary
{
    public class EmployeeDbContext :IdentityDbContext<Employee>
    {
        
        public EmployeeDbContext() : base("EmployeeDbContext")
        {

        }
        public DbSet<Leave> Leaves { get; set; }

    }
}
