namespace FullArcheticture_WebApiLab.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IRepository<Department> DepartmentRepository { get; }
        public IRepository<Employee> EmployeeRepository { get; }
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            DepartmentRepository = new GenericRepository<Department>(_context);
            EmployeeRepository = new GenericRepository<Employee>(_context);
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
