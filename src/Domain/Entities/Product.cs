using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PastasAPI.Domain.Entities;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Name { get; set; }
    public float Price { get; set; }
    public int Stock { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public bool IsAvailable { get; set; } = true;
}