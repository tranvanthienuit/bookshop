using Microsoft.AspNetCore.Identity;

namespace bookshop.Entity;

public class User : IdentityUser
{
    public String fullname { get; set; }
    public String address { get; set; }
    public DateTime dayAdd { get; set; }= DateTime.Now;
    public String sex { get; set; }
    public Byte[] image { get; set; }
}