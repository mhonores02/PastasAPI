namespace PastasAPI.Application.Models.Requests;

public class ProductCreateRequest
{
    public string? Name { get; set; }
    public float Price { get; set; }
    public int Stock { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
}