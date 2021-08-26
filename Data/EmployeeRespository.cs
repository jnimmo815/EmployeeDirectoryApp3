using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Data
{
    public class EmployeeRespository : IEmployeeRepository
    {
        public void Delete(Employee obj)
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
                db.Employees.Attach(obj);
                db.Employees.Remove(obj);
                db.SaveChanges();
            }
        }

        public List<Employee> GetAll()
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
                return db.Employees.ToList();
            }
        }

        public Employee GetById(int id)
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
                return db.Employees.Find(id);
            }
        }

        public Employee Insert(Employee obj)
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
                db.Employees.Add(obj);
                db.SaveChanges();
                return obj;
            }
        }

        public void Update(Employee obj)
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
                db.Employees.Attach(obj);
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();       
            }
        }
    }
}
