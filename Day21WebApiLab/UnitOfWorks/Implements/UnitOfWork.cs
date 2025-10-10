using Day21WebApiLab.Repositories.Implements;
using Day21WebApiLab.UnitOfWorks.Interfaces;

namespace Day21WebApiLab.UnitOfWorks.Implements
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IRepository<Department> DepartmentRepository { get; }
        public IRepository<Employee> EmployeeRepository { get; }
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            DepartmentRepository = new Repository<Department>(_context);
            EmployeeRepository = new Repository<Employee>(_context);
        }
        public int Complete()
        {
            var rows = _context.SaveChanges(); // return int 
            _context.ChangeTracker.Clear();//.State = EntityState.Detached;
            return rows;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
