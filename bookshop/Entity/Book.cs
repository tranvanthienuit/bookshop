using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookshop.Entity;

public class Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public String bookId{ get; set; }
    public String nameBook{ get; set; }
    public String author{ get; set; }
    public String publish{ get; set; }
    public DateTime dayAdd { get; set; }=DateTime.Now;
    public Double price{ get; set; }
    public int count{ get; set; }
    public Byte[] image { get; set; }
    public Category Category { get; set; }
    public List<OrderDe> OrderDes{ get; set; }
    // public List<Comment> Comments{ get; set; }
    // public List<Rate> Rates{ get; set; }
}