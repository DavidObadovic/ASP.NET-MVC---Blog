namespace MojProjekatPonovo.Repositories
{
    public interface IImageRepository
    {
        Task<string> uploadAsync(IFormFile file);
    }
}
