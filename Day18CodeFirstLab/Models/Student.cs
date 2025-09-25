using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Day18CodeFirstLab.Models
{
    public class Student
    {
        //Convention over configuration : Table name is pluralized by default by EF Core
        // Property name "Id" or "ClassNameId" is considered as Primary key by convention
        // Data Annotations and Fluent API can be used to override the convention
        // Data Annotations are attributes that can be applied to classes and properties to configure the model
        // Fluent API is a way to configure the model using code in the DbContext class
        // Data Annotations are limited in their capabilities and Fluent API is more powerful and flexible
        // Data Annotations are applied to the class and properties directly
        // Fluent API is applied in the OnModelCreating method of the DbContext class

        [Key] //Data Annotation to specify primary key
        public int StudentId { get; set; } //Primary key by convention

        [Required(ErrorMessage ="Must Enter a Name")] //Data Annotation to specify that the property is required (NOT NULL)
        [StringLength(50 , MinimumLength =3)] //Data Annotation to specify the maximum length of the property
        public string Name { get; set; } //Required by convention (string is nullable by default)

        [DisplayFormat(DataFormatString ="{0:P}")] // Percentage format
        public double Score { get; set; } //Not Required by convention (double is not nullable by default)

        [StringLength(10, MinimumLength = 6)]
        public string Code { get; set; } //Not Required by convention (string is nullable by default)

        [Range(18, 25, ErrorMessage ="Age must be between 18 and 25")]
        public int Age { get; set; } //Not Required by convention (int is not nullable by default)

        [RegularExpression("^[a-zA-Z)]{6}$" , ErrorMessage ="Group must be 6 alphabetic characters")]
        public string Group { get; set; } //Not Required by convention (string is nullable by default)

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; } //Not Required by convention (string is nullable by default)

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } //Not Required by convention (string is nullable by default)

        [IgnoreDataMember] //Data Annotation to ignore the property in the database
        [FileExtensions(Extensions =".jpg,.png,.gif", ErrorMessage ="Only image files are allowed")]
        public byte []? photoFile { get; set; } //Not Required by convention (byte array is nullable by default)
        // here ? means nullable

        public string? photoPath { get; set; } //Not Required by convention (string is nullable by default)
        
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString ="{0:MM/dd/YYYY}", ApplyFormatInEditMode =true)]
        public DateTime? CreateDate { get; set; } //Not Required by convention (DateTime is not nullable by default)
        
        [Range(typeof(decimal), "1000.50", "100000.50")]
        [CreditCard] //Data Annotation to specify that the property is a credit card number
        public Decimal Credit { get; set; } //Not Required by convention (Decimal is not nullable by default)
    }
}
