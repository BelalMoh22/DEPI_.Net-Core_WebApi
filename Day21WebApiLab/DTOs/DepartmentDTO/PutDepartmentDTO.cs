using System.ComponentModel.DataAnnotations;

namespace Day21WebApiLab.DTOs.DepartmentDTO
{
 public class PutDepartmentDTO
    {
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Must Enter Name...")]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public string? ManagerName { get; set; }
    }
}
