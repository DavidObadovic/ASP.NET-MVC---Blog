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

        public Task<BlogPost?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await dbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await dbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<BlogPost?> UpdateAsync(BlogPost post)
        {
            throw new NotImplementedException();
        }
    }
}
