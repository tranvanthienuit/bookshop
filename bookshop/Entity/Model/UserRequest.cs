using System.Text;

namespace bookshop.Entity.Model;

public class UserRequest
{
    public String username { get; set; }
    public String password { get; set; }
    public String fullname { get; set; }
    public String address { get; set; }
    public String sex { get; set; }
    public Byte[] image { get; set; }
}