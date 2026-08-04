using Microsoft.EntityFrameworkCore;
using PastasAPI.Domain.Entities;
using PastasAPI.Domain.Interfaces;

namespace PastasAPI.Infrastructure.Data;

public class CartRepository : BaseRepository<Cart>, ICartRepository
{
    public CartRepository(ApplicationContext context) : base(context) { }

    public Cart? GetCartByClientId(int clientId)
    {
        return _context.Carts
            .Include(c => c.Products)
            .FirstOrDefault(c => c.ClientId == clientId);
    }

    public Cart CreateForClient(int clientId)
    {
        var cart = new Cart { ClientId = clientId };
        _context.Carts.Add(cart);
        _context.SaveChanges();
        return cart;
    }
}