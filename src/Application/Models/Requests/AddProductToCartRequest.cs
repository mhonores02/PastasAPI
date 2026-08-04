namespace PastasAPI.Application.Models.Requests;

public class AddProductToCartRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; } = 1;
}