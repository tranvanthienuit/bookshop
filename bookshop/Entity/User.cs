﻿using Microsoft.AspNetCore.Identity;

namespace bookshop.Entity;

public class User : IdentityUser
{
    public String fullname { get; set; }
    public String address { get; set; }
    public DateTime dayAdd { get; set; }= DateTime.Now;
    public String sex { get; set; }
    public Byte[] image { get; set; }
    public List<Comment> Comments{ get; set; }
    public List<Rate> Rates{ get; set; }
    public List<Order> Orders{ get; set; }
    public List<Blog> Blogs{ get; set; }
}