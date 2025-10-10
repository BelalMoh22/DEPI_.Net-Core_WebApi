using Day21WebApiLab.Data;
using Day21WebApiLab.Models;
using Microsoft.EntityFrameworkCore;

// Day 22
namespace Day21WebApiLab.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Department> GetAll()
        {
            var departments = _context.Departments.ToList();
            return departments;
        }

        public Department GetByID(int id)
        {
            var department = _context.Departments.FirstOrDefault(d => d.DepartmentId == id);

            if (department == null)
            {
                return null;
            }
            else
            {
                return department;
            }
        }

        public void Add(Department department) 
        {
            _context.Departments.Add(department);
            //_context.SaveChanges();
        }

        public void Update(Department department) 
        {
            _context.Entry(department).State = EntityState.Modified;
            //_context.SaveChanges();
        }

        public void Delete(int id) 
        {
            var department = _context.Departments.Find(id);
            if (department != null) 
            {
                _context.Departments.Remove(department);
                //_context.SaveChanges();
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
