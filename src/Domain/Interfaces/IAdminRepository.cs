using PastasAPI.Domain.Entities;

namespace PastasAPI.Domain.Interfaces;

public interface IAdminRepository : IBaseRepository<Admin>
{
    Admin? GetByEmail(string email);
}