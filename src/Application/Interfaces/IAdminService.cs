using PastasAPI.Application.Models;
using PastasAPI.Application.Models.Requests;

namespace PastasAPI.Application.Interfaces;

public interface IAdminService
{
    ICollection<AdminDto> GetAll();
    AdminDto GetById(int id);
    AdminDto Create(AdminCreateRequest request);
    AdminDto Update(int id, AdminUpdateRequest request);
    void Delete(int id);
}