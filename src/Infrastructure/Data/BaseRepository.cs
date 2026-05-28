using Microsoft.EntityFrameworkCore;
using PastasAPI.Domain.Interfaces;

namespace PastasAPI.Infrastructure.Data;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly ApplicationContext _context;

    public BaseRepository(ApplicationContext context)
    {
        _context = context;
    }

    public ICollection<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public T? GetById(int id)
    {
        return _context.Set<T>().Find(id);
    }

    public T Add(T entity)
    {
        _context.Set<T>().Add(entity);
        return entity;
    }

    public T Update(T entity)
    {
        _context.Set<T>().Update(entity);
        return entity;
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity != null)
            _context.Set<T>().Remove(entity);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}