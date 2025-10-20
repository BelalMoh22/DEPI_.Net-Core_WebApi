namespace FullArcheticture_WebApiLab.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IRepository<Department> DepartmentRepository { get; }
        public IRepository<Employee> EmployeeRepository { get; }
        public IRepository<Customer> CustomerRepository { get; }
        public IRepository<Category> CategoryRepository { get; }
        public IRepository<Product> ProductRepository { get; }
        public IRepository<Order> OrderRepository { get; }
        public IRepository<OrderItem> OrderItemRepository { get; }

        public UnitOfWork(AppDbContext context,
                          IRepository<Department> departmentRepository,
                          IRepository<Employee> employeeRepository,
                          IRepository<Customer> customerRepository,
                          IRepository<Category> categoryRepository,
                          IRepository<Product> productRepository,
                          IRepository<Order> orderRepository,
                          IRepository<OrderItem> orderItemRepository)
        {
            _context = context;
            DepartmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
            EmployeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            CustomerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            CategoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            ProductRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            OrderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            OrderItemRepository = orderItemRepository ?? throw new ArgumentNullException(nameof(orderItemRepository));
            //DepartmentRepository = new GenericRepository<Department>(_context);
            //EmployeeRepository = new GenericRepository<Employee>(_context);
        }

        public int Complete()
        {
            var rows = _context.SaveChanges();
            _context.ChangeTracker.Clear();//.State = EntityState.Detached;
            return rows;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
