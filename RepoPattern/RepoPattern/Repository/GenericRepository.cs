using Microsoft.EntityFrameworkCore;
using RepoPattern.Entities;

namespace RepoPattern.Repository;

public class GenericRepository<T> : IRepository<T> where T : Entity
{
    protected readonly DbContext _context;

    public GenericRepository(DbContext context)
    {
        _context = context;
    }

    public T? Add(T entity)
    {
        try
        {
            var result = _context.Set<T>()?.Add(entity).Entity;
            _context.SaveChanges();

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.InnerException?.ToString());
        }
    }

    public bool Update(T entity)
    {
        try
        {
            var local = _context.Set<T>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entity.Id));

            // check if local is not null 
            if (local is not null)
            {
                // detach
                _context.Entry(local).State = EntityState.Detached;
            }
            // set Modified flag in your entry
            _context.Entry(entity).State = EntityState.Modified;

            // save 
            _context.SaveChanges();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}{ex.InnerException?.ToString()}");
        }

    }

    public T? Get(Func<T, bool> predicate)
    {
        return _context.Set<T>()?.Where(predicate).FirstOrDefault();
    }

    public List<T>? GetEntities(Func<T, bool> predicate)
    {
        return _context.Set<T>()?.Where(predicate).ToList();
    }

    public bool Remove(T entity)
    {
        try
        {
            _context.Set<T>()?.Remove(entity);
            _context.SaveChanges();

            return true;

        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}{ex.InnerException?.ToString()}");
        }
    }

    public int CountAllEntries()
    {
        return _context.Set<T>()?.Count() ?? 0;
    }

    public T? GetById(Guid id)
    {
        return _context.Set<T>()?.Find(id);
    }

    public List<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }
}

