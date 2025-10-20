using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullArcheticture_WebApiLab.Models
{
    [Table("TblOrders")]
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Must Enter Order Date")]
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public bool Status { get; set; } = false;
        public string? ShippingAddress { get; set; }

        // Navigation Property
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        // Navigation Property
        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }
}