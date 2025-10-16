using System.Linq.Expressions;
using FullArcheticture_WebApiLab.DTOs.DepartmentDtos;

namespace FullArcheticture_WebApiLab.Services.Interface
{
   public enum ExistType
    {
        Create = 1,
        Update = 2
    }
    public interface IServiceDepartment
    {
        //Logical Business
        IEnumerable<GetDepartmentDto> GetDepartments();
        GetDepartmentDto GetDepartmentByID(int id);
        void AddDepartment(PostDepartmentDto department);
        void UpdateDepartment(PutDepartmentDto department);
        void DeleteDepartment(int id);
        int GetDepartmentCounter();
        IEnumerable<GetDepartmentDto> PaginationDepartments(int page = 1, int pageSize = 10);
        IEnumerable<GetDepartmentDto> SearchDepartment(Expression<Func<Department, bool>> predicate);
        IEnumerable<GetDepartmentWithEmpNamesDto> DepartmentWithEmployee(params string[] including);
        int GetMaxID();

        bool isExist(CheckDepartmentDto department, ExistType existType = ExistType.Create);
    }
}
