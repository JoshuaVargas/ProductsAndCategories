#pragma warning disable CS8618
namespace ProductsAndCategories.Models;

public class MyViewModel
{
    public Product Product { get; set;}
    public List<Product> AllProducts { get; set; }

    public Category Category { get; set; }
    public List<Category> AllCategories { get; set; }

    public Association Association { get; set; }
    public List<Association> AllAssociations { get; set; }
}