using LMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositary
{
    public class LeaveRepository
    {
        EmployeeDbContext db;



        public LeaveRepository()
        {
            db = new EmployeeDbContext();


        }

        public void Register(Leave obj)
        {
            obj.Number = (obj.EndDate - obj.StartDate).Days;

            db.Leaves.Add(obj);
            db.SaveChanges();

        }

        public List<Leave> GetLeaves()
        {
            return db.Leaves.Select(temp => temp).ToList();
        }

        public Leave GetLeave(int Id)
        {
            return db.Leaves.Where(temp => temp.Lid == Id).FirstOrDefault();
        }

        public void Update(Leave lv)
        {
            var l = db.Leaves.Where(temp => temp.Lid == lv.Lid).FirstOrDefault();
            l.Status = lv.Status;

            db.SaveChanges();
        }


    }
}
