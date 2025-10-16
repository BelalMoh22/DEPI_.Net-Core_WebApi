using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FullArcheticture_WebApiLab.Repository.Implement
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;
        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<T> GetAll()
        {
            return _appDbContext.Set<T>().AsNoTracking().ToList();
        }

        public T GetById(int id)
        {
            return _appDbContext.Set<T>().Find(id);
        }

        //public IEnumerable<T> GetWithPagination(int page = 1, int pageSize = 10)
        //{
        //    IEnumerable<T> list = _appDbContext.Set<T>().AsNoTracking().ToList();
        //    //Pagination
        //    var totalCount = _appDbContext.Set<T>().Count();
        //    var totalPage = (int)Math.Ceiling((double)totalCount / pageSize);
        //    list = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        //    return list;
        //}
        public IEnumerable<T> GetWithPagination(int page = 1, int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

           // IQueryable<T> query = _appDbContext.Set<T>().AsNoTracking(); // All rows also
            var pagedList = _appDbContext.Set<T>().AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            return pagedList.ToList();
        }

        public int Counter()
        {
            return _appDbContext.Set<T>().Count();
        }

        public void Add(T entity)
        {
            _appDbContext.Set<T>().Add(entity);
            //_appDbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            //_appDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            T entity = _appDbContext.Set<T>().Find(id);
            _appDbContext.Set<T>().Remove(entity);
            //_appDbContext.SaveChanges();
        }

        public IEnumerable<T> Search(Expression<Func<T, bool>> predicate)
        {
            return _appDbContext.Set<T>().Where(predicate).AsNoTracking().ToList();
        }

        public IEnumerable<T> GetAllIncluding(params string[] includes) //("Employees"," Orders")
        {
            IQueryable<T> query = _appDbContext.Set<T>().AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.AsNoTracking().ToList();
            /*
             query = _appDbContext.department.Include("Employee")
                                             .Include("Orders").AsNoTracking.Tolist() 
            */
        }
        public int GetMaxId()
        {
            var keyName = _appDbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Select(x => x.Name).Single();
            // Use EF Core's Find method instead of reflection
            return _appDbContext.Set<T>().AsNoTracking().Max(e => EF.Property<int>(e, keyName));
            //_appDbContext.Employees.Max(e => e.Id);
        }
    }
}
