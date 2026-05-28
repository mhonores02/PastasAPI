using PastasAPI.Application.Models;
using PastasAPI.Application.Models.Requests;

namespace PastasAPI.Application.Interfaces;

public interface IProductService
{
    ICollection<ProductDto> GetAll();
    ICollection<ProductDto> GetByCategory(string category);
    ProductDto GetById(int id);
    ProductDto Create(ProductCreateRequest request);
    ProductDto Update(int id, ProductUpdateRequest request);
    void Delete(int id);
}