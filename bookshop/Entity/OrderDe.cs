using System.ComponentModel.DataAnnotations;

namespace bookshop.Entity;

public class OrderDe
{
    [Key]
    public String orderDeId { get; set; }
    public int count { get; set; }
    public Double totalPrice { get; set; }
    public Book Book { get; set; }
    public Order Order { get; set; }
}