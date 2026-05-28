using Microsoft.EntityFrameworkCore;
using PastasAPI.Domain.Entities;
using PastasAPI.Domain.Interfaces;

namespace PastasAPI.Infrastructure.Data;

public class ClientRepositoryEf : BaseRepository<Client>, IClientRepository
{
    public ClientRepositoryEf(ApplicationContext context) : base(context) { }

    public Client? GetByEmail(string email)
    {
        return _context.Clients
            .FirstOrDefault(c => c.Email == email);
    }
}