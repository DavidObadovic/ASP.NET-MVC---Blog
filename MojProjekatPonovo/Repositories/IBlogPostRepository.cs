using MojProjekatPonovo.Models.Domain;

namespace MojProjekatPonovo.Repositories
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetAsync(Guid id);
        Task<BlogPost> AddAsync(BlogPost post);
        Task<BlogPost?> UpdateAsync(BlogPost post);
        Task<BlogPost?> DeleteAsync(Guid id);
    }
}
