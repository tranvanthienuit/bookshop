namespace bookshop.Entity;

public class Book
{
    public String bookId{ get; set; }
    public String nameBook{ get; set; }
    public String author{ get; set; }
    public String publish{ get; set; }
    public DateTime dayAdd { get; set; }=DateTime.Now;
    public Double price{ get; set; }
    public int count{ get; set; }
}