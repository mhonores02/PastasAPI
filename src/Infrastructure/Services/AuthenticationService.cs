using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PastasAPI.Application.Interfaces;
using PastasAPI.Application.Models.Requests;
using PastasAPI.Domain.Exceptions;
using PastasAPI.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PastasAPI.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IAdminRepository _adminRepository;
    private readonly IClientRepository _clientRepository;
    private readonly AuthenticationServiceOptions _options;

    public AuthenticationService(IAdminRepository adminRepository, IClientRepository clientRepository, IOptions<AuthenticationServiceOptions> options)
    {
        _adminRepository = adminRepository;
        _clientRepository = clientRepository;
        _options = options.Value;
    }

    public string Authenticate(CredentialsRequest credentials)
    {
        // Buscar en admins primero
        var admin = _adminRepository.GetByEmail(credentials.Email ?? "");
        if (admin != null && admin.Password == credentials.Password)
            return GenerateToken(admin.Id.ToString(), admin.Email ?? "", admin.Rol.ToString());

        // Buscar en clientes
        var client = _clientRepository.GetByEmail(credentials.Email ?? "");
        if (client != null && client.Password == credentials.Password)
            return GenerateToken(client.Id.ToString(), client.Email ?? "", client.Rol.ToString());

        throw new NotAllowedException("Email o contraseña incorrectos.");
    }

    private string GenerateToken(string id, string email, string rol)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretForKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, id),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, rol)
        };

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public class AuthenticationServiceOptions
    {
        public const string AuthenticationService = "AuthenticationService";
        public string SecretForKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
    }
}