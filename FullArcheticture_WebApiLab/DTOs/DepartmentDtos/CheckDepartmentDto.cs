using System.ComponentModel.DataAnnotations;

namespace FullArcheticture_WebApiLab.DTOs.DepartmentDtos
{
    public class CheckDepartmentDto
    {
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Must Enter Name")]
        [MaxLength(150)]
        public string Name { get; set; }
    }
}
