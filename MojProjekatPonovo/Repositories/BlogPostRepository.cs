using Microsoft.EntityFrameworkCore;
using MojProjekatPonovo.Data;
using MojProjekatPonovo.Models.Domain;

namespace MojProjekatPonovo.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private BlogDbContext dbContext;
        public BlogPostRepository(BlogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost post)
        {
            await dbContext.BlogPosts.AddAsync(post);
            await dbContext.SaveChangesAsync();
            return post;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var exist = await dbContext.BlogPosts.FindAsync(id);
            if (exist != null)
            {
                dbContext.BlogPosts.Remove(exist);
                await dbContext.SaveChangesAsync();
                return exist;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await dbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await dbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost post)
        {
            var exist = await dbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == post.Id);
            if (exist != null)
            {
                exist.Id = post.Id;
                exist.Heading = post.Heading;
                exist.PageTitle = post.PageTitle;
                exist.Contect = post.Contect;
                exist.ShortDecription = post.ShortDecription;
                exist.Author = post.Author;
                exist.FeaturedImageUrl = post.FeaturedImageUrl;
                exist.UrlHandle = post.UrlHandle;
                exist.Visible = post.Visible;
                exist.PublishedDate = post.PublishedDate;
                exist.Tags = post.Tags;

                await dbContext.SaveChangesAsync();
                return exist;
            }
            return null;
        }
    }
}
