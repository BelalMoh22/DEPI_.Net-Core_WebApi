namespace Day18WebApiCodeFirstLab.Models
{
    public partial class Student
    {
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Mark: {Mark}, Class: {Class}, IsDeleted: {IsDeleted}";
        }
    }
}
