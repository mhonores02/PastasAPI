using PastasAPI.Domain.Enums;

namespace PastasAPI.Application.Models;

public class CartDto
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public ICollection<PastasAPI.Domain.Entities.CartItem>? Items { get; set; }
    public float TotalPrice { get; set; }
    public CartEnum Status { get; set; }
    public string? PaymentMethod { get; set; }
}