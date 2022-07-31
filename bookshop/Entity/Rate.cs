namespace bookshop.Entity;

public class Rate
{
    public String rateId { get; set; }
    public int rate { get; set; }
    public User User { get; set; }
}