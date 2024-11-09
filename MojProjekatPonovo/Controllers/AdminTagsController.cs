using Microsoft.AspNetCore.Mvc;

namespace MojProjekatPonovo.Controllers
{
    public class AdminTagsController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
