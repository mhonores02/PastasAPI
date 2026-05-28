namespace PastasAPI.Application.Models.Requests;

public class ClientUpdateRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
}