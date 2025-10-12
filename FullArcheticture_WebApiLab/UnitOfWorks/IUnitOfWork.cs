namespace FullArcheticture_WebApiLab.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Department> DepartmentRepository { get; }
        IRepository<Employee> EmployeeRepository { get; }

        // IRepository<ModelName> EntityName { get; }
        int Complete();
    }
}
