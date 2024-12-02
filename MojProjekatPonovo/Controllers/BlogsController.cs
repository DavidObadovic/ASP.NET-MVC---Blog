using Microsoft.AspNetCore.Mvc;
using MojProjekatPonovo.Repositories;

namespace MojProjekatPonovo.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;

        public BlogsController(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogPost = await blogPostRepository.GetAsync(urlHandle);
            return View(blogPost);
        }
    }
}
