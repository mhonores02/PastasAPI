using PastasAPI.Application.Models.Requests;

namespace PastasAPI.Application.Interfaces;

public interface IAuthenticationService
{
    string Authenticate(CredentialsRequest credentials);
}