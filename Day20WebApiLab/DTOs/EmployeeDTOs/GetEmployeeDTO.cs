using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day20WebApiLab.DTOs.EmployeeDTOs
{
    public class GetEmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public decimal Salary { get; set; }
        public string departmentname { get; set; } // Add this property to fix CS0117
    }
}
