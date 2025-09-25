using Day18LabQuery.Models;
using Microsoft.EntityFrameworkCore;

namespace Day18LabQuery
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ADOTestContext db  = new ADOTestContext())
            {
                //var students = db.Students.ToList();
                var students = db.Students.AsNoTracking().ToList();
                foreach (var student in students)
                {
                    Console.WriteLine($"ID: {student.ID}, Name: {student.Name}, Mark: {student.Mark}, Class: {student.Class}, IsDeleted: {student.IsDeleted}, Subject: {student.Subject}");
                }    
                var studentById = db.Students.Find(3);
                Console.WriteLine($"Status {db.Entry(studentById).State}");
                studentById.Name = "Updated Name";
                Console.WriteLine($"Status {db.Entry(studentById).State}");
                Console.WriteLine("-------------------------------------------------------------");
                var m = db.Students.Find(5);
                Console.WriteLine($"Status {db.Entry(m).State}");
                db.Students.Remove(m);
                Console.WriteLine($"Status {db.Entry(m).State}");
                Console.WriteLine("-------------------------------------------------------------");
                var newStudent = new Student
                {
                    Name = "New Student",
                    Mark = 85,
                    Class = "10th Grade",
                    IsDeleted = false,
                    Subject = "Math"
                };
                Console.WriteLine($"Status {db.Entry(newStudent).State}");
                db.Students.Add(newStudent);
                Console.WriteLine($"Status {db.Entry(newStudent).State}");
                Console.WriteLine("------------------------------------");
                Console.WriteLine(db.ChangeTracker.ToDebugString());
                //db.SaveChanges();
            }
        }
    }
}
