using bookshop.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace bookshop.DbContext;

public class Dbcontext : IdentityDbContext<User>
{
    public Dbcontext(DbContextOptions<Dbcontext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Book> Books{ get; set; }
    public DbSet<Category> Categories{ get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Order> Orders{ get; set; }
    public DbSet<OrderDe> OrderDes { get; set; }
    public DbSet<Rate> Rates { get; set; }
}