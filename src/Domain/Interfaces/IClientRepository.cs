using PastasAPI.Domain.Entities;

namespace PastasAPI.Domain.Interfaces;

public interface IClientRepository : IBaseRepository<Client>
{
    Client? GetByEmail(string email);
}
