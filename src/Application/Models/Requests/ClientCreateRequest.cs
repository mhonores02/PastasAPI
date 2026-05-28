namespace PastasAPI.Application.Models.Requests;

public class ClientCreateRequest
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
}