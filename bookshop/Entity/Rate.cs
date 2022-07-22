using System.ComponentModel.DataAnnotations;

namespace bookshop.Entity;

public class Rate
{
    [Key]
    public String rateId { get; set; }
    public Double rate { get; set; }
    public Book Book { get; set; }
    public User User { get; set; }
}