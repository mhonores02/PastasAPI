using PastasAPI.Domain.Entities;
using PastasAPI.Domain.Interfaces;

namespace PastasAPI.Infrastructure.Data;

public class ProductRepositoryEf : BaseRepository<Product>, IProductRepository
{
    public ProductRepositoryEf(ApplicationContext context) : base(context) { }

    public ICollection<Product> GetByCategory(string category)
    {
        return _context.Products
            .Where(p => p.Category == category)
            .ToList();
    }
}