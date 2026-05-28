using PastasAPI.Application.Interfaces;
using PastasAPI.Application.Models;
using PastasAPI.Application.Models.Requests;
using PastasAPI.Domain.Entities;
using PastasAPI.Domain.Exceptions;
using PastasAPI.Domain.Interfaces;

namespace PastasAPI.Application.Services;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _adminRepository;

    public AdminService(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }

    public ICollection<AdminDto> GetAll()
    {
        var admins = _adminRepository.GetAll();
        return admins.Select(a => new AdminDto
        {
            Id = a.Id,
            Email = a.Email,
            Username = a.Username
        }).ToList();
    }

    public AdminDto GetById(int id)
    {
        var admin = _adminRepository.GetById(id)
            ?? throw new NotFoundException($"Admin con id {id} no encontrado.");

        return new AdminDto
        {
            Id = admin.Id,
            Email = admin.Email,
            Username = admin.Username
        };
    }

    public AdminDto Create(AdminCreateRequest request)
    {
        var admin = new Admin
        {
            Email = request.Email,
            Password = request.Password,
            Username = request.Username
        };

        _adminRepository.Add(admin);
        _adminRepository.SaveChanges();

        return new AdminDto
        {
            Id = admin.Id,
            Email = admin.Email,
            Username = admin.Username
        };
    }

    public AdminDto Update(int id, AdminUpdateRequest request)
    {
        var admin = _adminRepository.GetById(id)
            ?? throw new NotFoundException($"Admin con id {id} no encontrado.");

        admin.Email = request.Email;
        admin.Username = request.Username;

        _adminRepository.Update(admin);
        _adminRepository.SaveChanges();

        return new AdminDto
        {
            Id = admin.Id,
            Email = admin.Email,
            Username = admin.Username
        };
    }

    public void Delete(int id)
    {
        var admin = _adminRepository.GetById(id)
            ?? throw new NotFoundException($"Admin con id {id} no encontrado.");

        _adminRepository.Delete(id);
        _adminRepository.SaveChanges();
    }
}