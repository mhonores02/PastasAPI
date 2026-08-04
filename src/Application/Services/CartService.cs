using PastasAPI.Application.Interfaces;
using PastasAPI.Application.Models;
using PastasAPI.Application.Models.Requests;
using PastasAPI.Domain.Enums;
using PastasAPI.Domain.Exceptions;
using PastasAPI.Domain.Interfaces;

namespace PastasAPI.Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public CartService(ICartRepository cartRepository, IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }

    public CartDto GetByClientId(int clientId)
    {
        var cart = GetOrCreateCart(clientId);
        return MapToDto(cart);
    }

    public CartDto AddProduct(int clientId, AddProductToCartRequest request)
    {
        var cart = GetOrCreateCart(clientId);

        var product = _productRepository.GetById(request.ProductId)
            ?? throw new NotFoundException($"Producto con id {request.ProductId} no encontrado.");

        cart.Products ??= new List<PastasAPI.Domain.Entities.Product>();
        cart.Products.Add(product);
        cart.TotalPrice += product.Price;

        _cartRepository.Update(cart);
        _cartRepository.SaveChanges();

        return MapToDto(cart);
    }

    public CartDto RemoveProduct(int clientId, int productId)
    {
        var cart = GetOrCreateCart(clientId);

        var product = cart.Products?.FirstOrDefault(p => p.Id == productId)
            ?? throw new NotFoundException($"El producto {productId} no está en el carrito.");

        cart.Products!.Remove(product);
        cart.TotalPrice -= product.Price;

        _cartRepository.Update(cart);
        _cartRepository.SaveChanges();

        return MapToDto(cart);
    }

    public CartDto Checkout(int clientId)
    {
        var cart = GetOrCreateCart(clientId);

        if (cart.Products == null || !cart.Products.Any())
            throw new NotAllowedException("No se puede confirmar un carrito vacío.");

        cart.Status = CartEnum.Confirmed;

        _cartRepository.Update(cart);
        _cartRepository.SaveChanges();

        return MapToDto(cart);
    }

    private PastasAPI.Domain.Entities.Cart GetOrCreateCart(int clientId)
    {
        return _cartRepository.GetCartByClientId(clientId)
            ?? _cartRepository.CreateForClient(clientId);
    }

    private static CartDto MapToDto(PastasAPI.Domain.Entities.Cart cart) => new()
    {
        Id = cart.Id,
        ClientId = cart.ClientId,
        Products = cart.Products,
        TotalPrice = cart.TotalPrice,
        Status = cart.Status,
        PaymentMethod = cart.PaymentMethod
    };
}