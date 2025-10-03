using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day20WebApiLab.Models
{
    [Table("TblEmployees")]
    public class Employee
    {
        [Key]
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

        // Navigation Property
        //[ForeignKey("department")]
        // or
        [ForeignKey(nameof(department))]
        public int departmentId { get; set; }
        public virtual Department department { get; set; }
    }
}
