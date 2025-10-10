using System.Linq.Expressions;

namespace Day21WebApiLab.Repositories.Interfaces
{
    public interface IRepository<T> where T : class // or <T ,KeyId>
    {
        IEnumerable<T> GetAll();

        T GetbyID(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(int id);

        // Department.Find(d => d.name.contains"a")
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        int RowCount();

        //Use Include for Eager Loading
        IEnumerable<T> GetAllIncluding(params string[] includes);



    }
}
