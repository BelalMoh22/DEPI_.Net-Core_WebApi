using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day21WebApiLab.DTOs.EmployeeDTO
{
    public class GetEmployeeDTO
    {
        public string Name { get; set; }

        public string Job { get; set; }

        public string Salary { get; set; }

        public  string DepartmentName { get; set; }

        public string ManagerName { get; set; }
    }
}
