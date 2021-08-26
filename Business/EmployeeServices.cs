using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Model;

namespace Business
{
    public static class EmployeeServices
    {
        static IEmployeeRepository repository;

        static EmployeeServices()
        {
            repository = new EmployeeRespository();
        }

        public static List<Employee> GetAll()
        {
            return repository.GetAll();
        }

        public static Employee GetById(int id)
        {
            return repository.GetById(id);
        }

        public static Employee Insert(Employee obj)
        {
            return repository.Insert(obj);
        }

        public static void Update(Employee obj)
        {
            repository.Update(obj);
        }

        public static void Delete(Employee obj)
        {
            repository.Delete(obj);
        }
    }
}
