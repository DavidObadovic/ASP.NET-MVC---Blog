using Microsoft.AspNetCore.Mvc.Rendering;

namespace MojProjekatPonovo.Models.ViewModels
{
    public class AddBlogPostRequest
    {
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Contect { get; set; }
        public string ShortDecription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }
        //Za prikazivanje tagova
        public IEnumerable<SelectListItem> Tags { get; set; }
        //Za kupljenje tagova
        public string[] SelectedTags { get; set; } = Array.Empty<string>();

    }
}
