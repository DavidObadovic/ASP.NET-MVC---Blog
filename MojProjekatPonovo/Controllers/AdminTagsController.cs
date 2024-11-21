using Microsoft.AspNetCore.Mvc;
using MojProjekatPonovo.Data;
using MojProjekatPonovo.Models.Domain;
using MojProjekatPonovo.Models.ViewModels;

namespace MojProjekatPonovo.Controllers
{
    public class AdminTagsController : Controller
    {
        private BlogDbContext _blogDbContext;
        public AdminTagsController(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            var tag = new Tag
            {

                Name= addTagRequest.Name,
                DisplayName= addTagRequest.DisplayName,
            };

            _blogDbContext.Tags.Add(tag);
            _blogDbContext.SaveChanges();
            return RedirectToAction("List"); 
        }

        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {
            var tags = _blogDbContext.Tags.ToList();

            return View(tags);
        }
    }
}
