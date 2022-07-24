namespace bookshop.Entity.Model;

public class Response<T>
{
    public List<T> result { get; set; }
    public int totalBook { get; set; }
}