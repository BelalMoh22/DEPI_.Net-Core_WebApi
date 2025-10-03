using System.ComponentModel.DataAnnotations;

namespace Day20WebApiLab.DTOs.EmployeeDTOs
{
    public class PutEmployeeDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Job { get; set; }

        [Required]
        [DataType("decimal(9,2)")]
        [RegularExpression(@"^\d+.?\d{0,2}$")]
        [Range(0, 9999999.99)]
        public decimal Salary { get; set; }

        public int departmentId { get; set; }
    }
}
