using System.Text.Json.Serialization;

namespace PastasAPI.Domain.Entities;

public class Client : User
{
    public Client()
    {
        Rol = PastasAPI.Domain.Enums.RolEnum.Customer;
    }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }

    [JsonIgnore]
    public Cart? Cart { get; set; }
}