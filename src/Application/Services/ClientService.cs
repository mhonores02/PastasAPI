using PastasAPI.Application.Interfaces;
using PastasAPI.Application.Models;
using PastasAPI.Application.Models.Requests;
using PastasAPI.Domain.Entities;
using PastasAPI.Domain.Exceptions;
using PastasAPI.Domain.Interfaces;

namespace PastasAPI.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public ICollection<ClientDto> GetAll()
    {
        var clients = _clientRepository.GetAll();
        return clients.Select(c => new ClientDto
        {
            Id = c.Id,
            Email = c.Email,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Address = c.Address,
            PhoneNumber = c.PhoneNumber
        }).ToList();
    }

    public ClientDto GetById(int id)
    {
        var client = _clientRepository.GetById(id)
            ?? throw new NotFoundException($"Cliente con id {id} no encontrado.");

        return new ClientDto
        {
            Id = client.Id,
            Email = client.Email,
            FirstName = client.FirstName,
            LastName = client.LastName,
            Address = client.Address,
            PhoneNumber = client.PhoneNumber
        };
    }

    public ClientDto Create(ClientCreateRequest request)
    {
        var client = new Client
        {
            Email = request.Email,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Address = request.Address,
            PhoneNumber = request.PhoneNumber
        };

        _clientRepository.Add(client);
        _clientRepository.SaveChanges();

        return new ClientDto
        {
            Id = client.Id,
            Email = client.Email,
            FirstName = client.FirstName,
            LastName = client.LastName,
            Address = client.Address,
            PhoneNumber = client.PhoneNumber
        };
    }

    public ClientDto Update(int id, ClientUpdateRequest request)
    {
        var client = _clientRepository.GetById(id)
            ?? throw new NotFoundException($"Cliente con id {id} no encontrado.");

        client.FirstName = request.FirstName;
        client.LastName = request.LastName;
        client.Address = request.Address;
        client.PhoneNumber = request.PhoneNumber;

        _clientRepository.Update(client);
        _clientRepository.SaveChanges();

        return new ClientDto
        {
            Id = client.Id,
            Email = client.Email,
            FirstName = client.FirstName,
            LastName = client.LastName,
            Address = client.Address,
            PhoneNumber = client.PhoneNumber
        };
    }

    public void Delete(int id)
    {
        var client = _clientRepository.GetById(id)
            ?? throw new NotFoundException($"Cliente con id {id} no encontrado.");

        _clientRepository.Delete(id);
        _clientRepository.SaveChanges();
    }
}