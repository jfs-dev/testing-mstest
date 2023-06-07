using System.Linq.Expressions;

namespace project_core.Interfaces;

public interface IRepository<T> where T : class
{
    T Create(T entity);

    T Update(T entity);

    void Delete(T entity);

    T Get(params object[] key);

    IQueryable<T> GetAll();

    IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
}
