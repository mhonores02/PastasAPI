using PastasAPI.Domain.Entities;
using PastasAPI.Domain.Interfaces;

namespace PastasAPI.Infrastructure.Data;

public class AdminRepositoryEf : BaseRepository<Admin>, IAdminRepository
{
    public AdminRepositoryEf(ApplicationContext context) : base(context) { }

    public Admin? GetByEmail(string email)
    {
        return _context.Admins
            .FirstOrDefault(a => a.Email == email);
    }
}