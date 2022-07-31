namespace bookshop.Entity;

public class Comment
{
    public String cmtId { get; set; }
    public String content { get; set; }
    public DateTime dayAdd { get; set; }= DateTime.Now;
    public User User { get; set; }
    public Book Book { get; set; }
}