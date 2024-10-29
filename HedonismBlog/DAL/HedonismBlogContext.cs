using HedonismBlog.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HedonismBlog.DataAccess
{
    public class HedonismBlogContext : DbContext
    {
        public HedonismBlogContext(DbContextOptions<HedonismBlogContext> options)
: base(options)
        {
            //Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; }
        public  DbSet<Role> Roles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
