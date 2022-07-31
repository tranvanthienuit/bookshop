namespace bookshop.Entity;

public class Blog
{
    public String blogId { get; set; }
    public String title { get; set; }
    public String context { get; set; }
    public String content { get; set; }
    public DateTime dayAdd { get; set; }= DateTime.Now;
    public Byte[] image { get; set; }
    public User User { get; set; }
}