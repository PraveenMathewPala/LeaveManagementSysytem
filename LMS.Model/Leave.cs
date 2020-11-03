using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Model
{
   public class Leave
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Lid { get; set; }

        public int Eid { get; set; }

        public string EmployeeName { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Number { get; set; }

        public string DepartmentName { get; set; }

        public bool Status { get; set; }

        public int Projectid { get; set; }


    }
}
