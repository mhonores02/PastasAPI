using PastasAPI.Application.Interfaces;
using PastasAPI.Application.Models;
using PastasAPI.Application.Models.Requests;
using PastasAPI.Domain.Entities;
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

        if (!product.IsAvailable)
            throw new NotAllowedException($"El producto '{product.Name}' no está disponible.");

        if (product.Stock < request.Quantity)
            throw new NotAllowedException($"Stock insuficiente para '{product.Name}'. Disponible: {product.Stock}.");

        cart.Items ??= new List<CartItem>();
        var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == product.Id);

        if (existingItem != null)
        {
            existingItem.Quantity += request.Quantity;
        }
        else
        {
            cart.Items.Add(new CartItem
            {
                CartId = cart.Id,
                ProductId = product.Id,
                Quantity = request.Quantity
            });
        }

        cart.TotalPrice += product.Price * request.Quantity;

        _cartRepository.Update(cart);
        _cartRepository.SaveChanges();

        return MapToDto(cart);
    }

    public CartDto RemoveProduct(int clientId, int productId)
    {
        var cart = GetOrCreateCart(clientId);

        var item = cart.Items?.FirstOrDefault(i => i.ProductId == productId)
            ?? throw new NotFoundException($"El producto {productId} no está en el carrito.");

        cart.TotalPrice -= (item.Product?.Price ?? 0) * item.Quantity;
        cart.Items!.Remove(item);

        _cartRepository.Update(cart);
        _cartRepository.SaveChanges();

        return MapToDto(cart);
    }

    public CartDto Checkout(int clientId)
    {
        var cart = GetOrCreateCart(clientId);

        if (cart.Items == null || !cart.Items.Any())
            throw new NotAllowedException("No se puede confirmar un carrito vacío.");

        foreach (var item in cart.Items)
        {
            var product = _productRepository.GetById(item.ProductId)
                ?? throw new NotFoundException($"Producto {item.ProductId} no encontrado.");

            if (product.Stock < item.Quantity)
                throw new NotAllowedException($"Stock insuficiente para '{product.Name}'.");

            product.Stock -= item.Quantity;
            _productRepository.Update(product);
        }

        cart.Status = CartEnum.Confirmed;

        _cartRepository.Update(cart);
        _cartRepository.SaveChanges();

        return MapToDto(cart);
    }

    private Cart GetOrCreateCart(int clientId)
    {
        return _cartRepository.GetCartByClientId(clientId)
            ?? _cartRepository.CreateForClient(clientId);
    }

    private static CartDto MapToDto(Cart cart) => new()
    {
        Id = cart.Id,
        ClientId = cart.ClientId,
        Items = cart.Items,
        TotalPrice = cart.TotalPrice,
        Status = cart.Status,
        PaymentMethod = cart.PaymentMethod
    };
}