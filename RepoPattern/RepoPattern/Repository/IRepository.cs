namespace RepoPattern.Repository;
public interface IRepository<T> where T : class
{
    T? Add(T entity);
    bool Update(T entity);
    T? Get(Func<T, bool> predicate);
    T? GetById(Guid id);
    List<T> GetAll();
    List<T>? GetEntities(Func<T, bool> predicate);
    bool Remove(T entity);
    int CountAllEntries();
}