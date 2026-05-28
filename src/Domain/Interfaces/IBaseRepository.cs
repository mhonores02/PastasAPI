namespace PastasAPI.Domain.Interfaces;

public interface IBaseRepository<T> where T : class
{
    ICollection<T> GetAll();
    T? GetById(int id);
    T Add(T entity);
    T Update(T entity);
    void Delete(int id);
    void SaveChanges();
}