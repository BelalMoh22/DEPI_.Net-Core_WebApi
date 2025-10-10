using Day21WebApiLab.Models;
using Microsoft.EntityFrameworkCore;

namespace Day21WebApiLab.Repositories
{
    public class ListDepartmentRepository : IDepartmentRepository
    {
        private static List<Department> Departments = new List<Department>()
        {
            new Department() {DepartmentId = 1 , Name = "IT" , Description = "it"},
            new Department() { DepartmentId = 2 , Name = "Sales" , Description = "sales"},
            new Department() { DepartmentId = 3 , Name = "Software" , Description = "software"},
            new Department() { DepartmentId =4 , Name = "HR" , Description = "Hr"}
        };
        public IEnumerable<Department> GetAll()
        {
            return Departments.ToList();
        }

        public Department GetByID(int id)
        {
            var department = Departments.FirstOrDefault(d => d.DepartmentId == id);
            return department;
        }

        public void Add(Department department)
        {
            Departments.Add(department);
            //_context.SaveChanges();
        }

        public void Update(Department department)
        {
            
            //_context.SaveChanges();
        }

        public void Delete(int id)
        {
            var department = Departments.FirstOrDefault(d => d.DepartmentId == id);
            Departments.Remove(department);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
