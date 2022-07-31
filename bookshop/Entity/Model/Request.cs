

namespace bookshop.Entity.Model;
// user
public class UserRequest
{
    public String username { get; set; }
    public String password { get; set; }
    public String fullname { get; set; }
    public String address { get; set; }
    public String sex { get; set; }
    public String image { get; set; }
}

public class UserLogin
{
    public String username { get; set; }
    public String password { get; set; }
}
// sach
public class BookRequest
{
    public String bookId{ get; set; }
    public String nameBook{ get; set; }
    public String author{ get; set; }
    public String publishYear{ get; set; }
    public String publishCom{ get; set; }

    public Double price{ get; set; }
    public int count{ get; set; }
    public String image { get; set; }
}

public class BookBuy
{
    public Book book { get; set; }
    public double totalPrice { get; set; }
    public int totalBook { get; set; }
}
// loai sach

//mua sach
public class cartRequest
{
    public List<BookBuy> book { get; set; }
    public double totalPrice { get; set; }
    public int totalBook { get; set; }
}