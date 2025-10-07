namespace Day21WebApiLab.DTOs.DepartmentDTO
{
    public class GetDepartmentDTO
    {
        public int DepartmentId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}

