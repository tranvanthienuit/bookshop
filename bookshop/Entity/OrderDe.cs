using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookshop.Entity;

public class OrderDe
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public String orderDeId { get; set; }
    public int count { get; set; }
    public Double totalPrice { get; set; }
    public Book Book { get; set; }
    public Order? Order { get; set; }
}