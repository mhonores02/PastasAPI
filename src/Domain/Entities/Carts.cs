using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using PastasAPI.Domain.Enums;

namespace PastasAPI.Domain.Entities;

public class Cart
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey("Client")]
    public int ClientId { get; set; }

    public ICollection<Product>? Products { get; set; } = new List<Product>();
    public float TotalPrice { get; set; }
    public CartEnum Status { get; set; }
    public string? PaymentMethod { get; set; }

    [JsonIgnore]
    public Client? Client { get; set; }

    public Cart()
    {
        Products = new List<Product>();
        TotalPrice = 0;
        Status = CartEnum.Pending;
        PaymentMethod = string.Empty;
    }
}