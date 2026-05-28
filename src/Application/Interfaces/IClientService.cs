using PastasAPI.Application.Models;
using PastasAPI.Application.Models.Requests;

namespace PastasAPI.Application.Interfaces;

public interface IClientService
{
    ICollection<ClientDto> GetAll();
    ClientDto GetById(int id);
    ClientDto Create(ClientCreateRequest request);
    ClientDto Update(int id, ClientUpdateRequest request);
    void Delete(int id);
}