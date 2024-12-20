﻿using Microsoft.AspNetCore.Mvc;
using MojProjekatPonovo.Models.ViewModels;
using MojProjekatPonovo.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using MojProjekatPonovo.Models.Domain;

namespace MojProjekatPonovo.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private ITagRepository tagRepository;
        private IBlogPostRepository blogPostRepository;
        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;

        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tag = await tagRepository.GetAllAsync();
            var model = new AddBlogPostRequest()
            {
                Tags = tag.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest blogPostRequest)
        {
            var BlogPost = new BlogPost
            {
                Heading = blogPostRequest.Heading,
                PageTitle = blogPostRequest.PageTitle,
                Contect = blogPostRequest.Contect,
                ShortDecription = blogPostRequest.ShortDecription,
                FeaturedImageUrl = blogPostRequest.FeaturedImageUrl,
                UrlHandle = blogPostRequest.UrlHandle,
                PublishedDate = blogPostRequest.PublishedDate,
                Author = blogPostRequest.Author,
                Visible = blogPostRequest.Visible,
            };

            var selectedItem = new List<Tag>();

            foreach (var tag in blogPostRequest.SelectedTags)
            {
                var selectedtag = Guid.Parse(tag);
                var existedTag = await tagRepository.GetAsync(selectedtag);

                if (existedTag != null) selectedItem.Add(existedTag);
            }

            BlogPost.Tags = selectedItem;
            await blogPostRepository.AddAsync(BlogPost);

            return RedirectToAction("Add");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var BlogPost = await blogPostRepository.GetAllAsync();
            return View(BlogPost);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var element = await blogPostRepository.GetAsync(id);
            var tagsModel = await tagRepository.GetAllAsync();
            if (element != null)
            {
                var model = new EditBlogPostRequest
                {
                    Id = element.Id,
                    Heading = element.Heading,
                    PageTitle = element.PageTitle,
                    Contect = element.Contect,
                    Author = element.Author,
                    FeaturedImageUrl = element.FeaturedImageUrl,
                    UrlHandle = element.UrlHandle,
                    ShortDecription = element.ShortDecription,
                    PublishedDate = element.PublishedDate,
                    Visible = element.Visible,
                    Tags = tagsModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = element.Tags.Select(x => x.Id.ToString()).ToArray()

                };
                return View(model);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest blogPost)
        {
            var BlogPostDomain = new BlogPost()
            {
                Id = blogPost.Id,
                Heading = blogPost.Heading,
                PageTitle = blogPost.PageTitle,
                Contect = blogPost.Contect,
                Author = blogPost.Author,
                ShortDecription = blogPost.ShortDecription,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                PublishedDate = blogPost.PublishedDate,
                UrlHandle = blogPost.UrlHandle,
                Visible = blogPost.Visible,
            };
            var selectedTag = new List<Tag>();

            foreach (var selectedtag in blogPost.SelectedTags)
            {
                if (Guid.TryParse(selectedtag, out var tag))
                {
                    var found = await tagRepository.GetAsync(tag);

                    if (found != null)
                        selectedTag.Add(found);

                }
            }

            BlogPostDomain.Tags = selectedTag;

            var obj = await blogPostRepository.UpdateAsync(BlogPostDomain);

            if (obj != null)
            {
                return RedirectToAction("List");
            }

            return View(blogPost);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest blogPost)
        {
            var exist = await blogPostRepository.DeleteAsync(blogPost.Id);

            if (exist != null)
                return RedirectToAction("List");


            return RedirectToAction("List");

        }
    }
}
