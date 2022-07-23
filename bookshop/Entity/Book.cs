using System.ComponentModel.DataAnnotations;

namespace bookshop.Entity;

public class Book
{
    [Key]
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
    public List<Comment> Comments{ get; set; }
    public List<Rate> Rates{ get; set; }
}