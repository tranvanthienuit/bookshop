using System.ComponentModel.DataAnnotations;

namespace bookshop.Entity;

public class OrderDe
{
    [Key]
    public String orderId { get; set; }
    public int count { get; set; }
    public Double totalPrice { get; set; }
}