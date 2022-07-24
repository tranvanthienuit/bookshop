using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookshop.Entity;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public String orderId { get; set; }
    public DateTime dayAdd { get; set; } = DateTime.Now;
    public int totalBook { get; set; }
    public Double totalPrice { get; set; }
    public String telephone { get; set; }
    public String address { get; set; }
    public String status { get; set; }
    public String pay { get; set; }
    public String username { get; set; }
    public String fullname { get; set; }
    public User User { get; set; }
    public List<OrderDe> OrderDes { get; set; }
}