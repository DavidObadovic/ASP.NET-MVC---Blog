using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MojProjekatPonovo.Data;
using MojProjekatPonovo.Models.Domain;
using MojProjekatPonovo.Models.ViewModels;
using MojProjekatPonovo.Repositories;

namespace MojProjekatPonovo.Controllers
{
    public class AdminTagsController : Controller
    {
        private ITagRepository tagRepository;
        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
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

            await tagRepository.AddAsync(tag);
            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var tags = await tagRepository.GetAllAsync();
            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //1st method
            //var tag = _blogDbContext.Tags.Find(id);

            var tag = await tagRepository.GetAsync(id);
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
            var updateTag = await tagRepository.UpdateAsync(tag);
            if (updateTag != null)
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var tag = await tagRepository.DeleteAsync(editTagRequest.Id);
            if (tag != null)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("List");
        }
    }
}
