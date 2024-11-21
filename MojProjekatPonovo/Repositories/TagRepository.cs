using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MojProjekatPonovo.Data;
using MojProjekatPonovo.Models.Domain;

namespace MojProjekatPonovo.Repositories
{
    public class TagRepository : ITagRepository
    {
        private BlogDbContext db;
        public TagRepository(BlogDbContext blogDbContext)
        {
            this.db = blogDbContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await db.AddAsync(tag);
            await db.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var tag = await db.Tags.FindAsync(id);
            if (tag != null)
            {
                db.Tags.Remove(tag);
                await db.SaveChangesAsync();
                return tag;
            }
            return null;

        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await db.Tags.ToListAsync();
        }

        public async Task<Tag> GetAsync(Guid id)
        {
            return await db.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var exist = await db.Tags.FindAsync(tag.Id);
            if (exist != null)
            {
                exist.Name = tag.Name;
                exist.DisplayName = tag.DisplayName;
                await db.SaveChangesAsync();
                return exist;
            }
            return null;
        }
    }
}
