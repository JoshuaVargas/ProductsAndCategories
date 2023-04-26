#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProductsAndCategories.Models;
public class Category
{
    [Key]
    public int CategoryId { get; set; }
    [Required (ErrorMessage = "is required")]
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public List<Product> CategoryProducts { get; set; } = new List<Product>();

    public List<Association> Products { get; set; } = new List<Association>();
}