using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookshop.Entity;

public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public String categoryId { get; set; }
    public String categoryName { get; set; }
    public List<Book> Books { get; set; }
}