namespace FullArcheticture_WebApiLab.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {

        // IRepository<ModelName> EntityName { get; }

        IRepository<Department> DepartmentRepository { get; }
        IRepository<Employee> EmployeeRepository { get; }
        
        IRepository<Customer> CustomerRepository { get; }

        IRepository<Order> OrderRepository { get; }

        IRepository<Category> CategoryRepository { get; }

        IRepository<Product> ProductRepository { get; }

        IRepository<OrderItem> OrderItemRepository { get; }

        int Complete();
    }
}
