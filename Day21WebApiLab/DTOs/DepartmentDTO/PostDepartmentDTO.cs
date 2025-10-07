using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Day21WebApiLab.DTOs.DepartmentDTO
{
    public class PostDepartmentDTO
    {
        [JsonIgnore]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Must Enter Name...")]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public string? ManagerName { get; set; }
    }
}
