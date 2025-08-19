using Microsoft.AspNetCore.Mvc;

namespace BlogsAPI.Controllers
{
    public class CommentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
