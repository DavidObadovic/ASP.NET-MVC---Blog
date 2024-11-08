using Microsoft.EntityFrameworkCore;
using MojProjekatPonovo.Models.Domain;

namespace MojProjekatPonovo.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
