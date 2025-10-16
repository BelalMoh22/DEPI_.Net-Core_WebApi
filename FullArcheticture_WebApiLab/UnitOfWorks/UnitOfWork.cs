namespace FullArcheticture_WebApiLab.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IRepository<Department> DepartmentRepository { get; }
        public IRepository<Employee> EmployeeRepository { get; }
        public UnitOfWork(AppDbContext context , IRepository<Department> departmentRepository , IRepository<Employee> employeeRepository)
        {
            _context = context;
            DepartmentRepository = departmentRepository?? throw new ArgumentNullException(nameof(departmentRepository));
            EmployeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
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
