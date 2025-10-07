using System.Text.Json.Serialization;

namespace Day21WebApiLab.DTOs.DepartmentDTO
{
    public class GetDepartmentWithEmployeeNameDTO
    {
        [JsonIgnore]
        public int DepartmentId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int EmployeeCounter { get; set; }
        public List<string> EmployeeNames { get; set; } = new List<string>();
    }
}
