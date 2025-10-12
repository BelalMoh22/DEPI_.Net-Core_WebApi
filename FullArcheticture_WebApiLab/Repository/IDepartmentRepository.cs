using FullArcheticture_WebApiLab.Models;

namespace FullArcheticture_WebApiLab.Repository
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        Department GetById(int id);
        void Add(Department department);
        void Update(Department department);
        void delete(int id);
        void Save();
    }
}
