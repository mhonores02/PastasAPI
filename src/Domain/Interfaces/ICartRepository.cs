using PastasAPI.Domain.Entities;

namespace PastasAPI.Domain.Interfaces;

public interface ICartRepository : IBaseRepository<Cart>
{
    Cart? GetCartByClientId(int clientId);
    Cart CreateForClient(int clientId);
}