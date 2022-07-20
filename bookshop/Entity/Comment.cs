namespace bookshop.Entity;

public class Comment
{
    public String commentId { get; set; }
    public String content { get; set; }
    public DateTime dayAdd { get; set; } = DateTime.Now;
}