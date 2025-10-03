using System.ComponentModel.DataAnnotations;

namespace Day20WebApiLab.DTOs.DepartmentDTOs
{
    public class GetDepartmentDTO
    {
        public int DepartmentId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
