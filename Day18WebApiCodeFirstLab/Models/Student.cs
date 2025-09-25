using System;
using System.Collections.Generic;

namespace Day18WebApiCodeFirstLab.Models;

public partial class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Mark { get; set; }

    public string? Class { get; set; }

    public bool IsDeleted { get; set; }

    public string? Subject { get; set; }
}
