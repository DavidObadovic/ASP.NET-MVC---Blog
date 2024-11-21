using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            var tag = new Tag
            {

                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            await _blogDbContext.Tags.AddAsync(tag);
            await _blogDbContext.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var tags = await _blogDbContext.Tags.ToListAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //1st method
            //var tag = _blogDbContext.Tags.Find(id);

            var tag = await _blogDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
            if (tag != null)
            {
                var editTag = new EditTagRequest();
                editTag.Name = tag.Name;
                editTag.DisplayName = tag.DisplayName;
                editTag.Id = tag.Id;
                return View(editTag);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag()
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };
            var existTag = await _blogDbContext.Tags.FindAsync(tag.Id);
            if (existTag != null)
            {
                existTag.Name = tag.Name;
                existTag.DisplayName = tag.DisplayName;
                await _blogDbContext.SaveChangesAsync();
                return RedirectToAction("List");
            }


            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var tag = await _blogDbContext.Tags.FindAsync(editTagRequest.Id);
            if (tag != null)
            {
                _blogDbContext.Remove(tag);
                await _blogDbContext.SaveChangesAsync();
                //show succes
                return RedirectToAction("List");
            }
            return RedirectToAction("List");
        }
    }
}
