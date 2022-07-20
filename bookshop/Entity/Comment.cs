using System.ComponentModel.DataAnnotations;

namespace bookshop.Entity;

public class Comment
{
    [Key]
    public String commentId { get; set; }
    public String content { get; set; }
    public DateTime dayAdd { get; set; } = DateTime.Now;
}