using PastasAPI.Domain.Entities;

namespace PastasAPI.Domain.Interfaces;

public interface IProductRepository : IBaseRepository<Product>
{
    ICollection<Product> GetByCategory(string category);
}