using PastasAPI.Application.Interfaces;
using PastasAPI.Application.Models;
using PastasAPI.Application.Models.Requests;
using PastasAPI.Domain.Entities;
using PastasAPI.Domain.Exceptions;
using PastasAPI.Domain.Interfaces;

namespace PastasAPI.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public ICollection<ProductDto> GetAll()
    {
        var products = _productRepository.GetAll();
        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Stock = p.Stock,
            Description = p.Description,
            Category = p.Category,
            IsAvailable = p.IsAvailable
        }).ToList();
    }

    public ICollection<ProductDto> GetByCategory(string category)
    {
        var products = _productRepository.GetByCategory(category);
        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Stock = p.Stock,
            Description = p.Description,
            Category = p.Category,
            IsAvailable = p.IsAvailable
        }).ToList();
    }

    public ProductDto GetById(int id)
    {
        var product = _productRepository.GetById(id)
            ?? throw new NotFoundException($"Producto con id {id} no encontrado.");

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Stock = product.Stock,
            Description = product.Description,
            Category = product.Category,
            IsAvailable = product.IsAvailable
        };
    }

    public ProductDto Create(ProductCreateRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock,
            Description = request.Description,
            Category = request.Category,
            IsAvailable = true
        };

        _productRepository.Add(product);
        _productRepository.SaveChanges();

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Stock = product.Stock,
            Description = product.Description,
            Category = product.Category,
            IsAvailable = product.IsAvailable
        };
    }

    public ProductDto Update(int id, ProductUpdateRequest request)
    {
        var product = _productRepository.GetById(id)
            ?? throw new NotFoundException($"Producto con id {id} no encontrado.");

        product.Name = request.Name;
        product.Price = request.Price;
        product.Stock = request.Stock;
        product.Description = request.Description;
        product.Category = request.Category;
        product.IsAvailable = request.IsAvailable;

        _productRepository.Update(product);
        _productRepository.SaveChanges();

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Stock = product.Stock,
            Description = product.Description,
            Category = product.Category,
            IsAvailable = product.IsAvailable
        };
    }

    public void Delete(int id)
    {
        var product = _productRepository.GetById(id)
            ?? throw new NotFoundException($"Producto con id {id} no encontrado.");

        _productRepository.Delete(id);
        _productRepository.SaveChanges();
    }
}