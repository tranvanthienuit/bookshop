using System.ComponentModel.DataAnnotations;

namespace bookshop.Entity;

public class Category
{
    [Key]
    public String categoryId { get; set; }
    public String categoryName { get; set; }
    
}