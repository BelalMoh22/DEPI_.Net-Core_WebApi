namespace Day21WebApiLab.UnitOfWorks.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Department> DepartmentRepository { get; }
        IRepository<Employee> EmployeeRepository { get; }
        int Complete();
    }
}
