using Microsoft.AspNetCore.Mvc;

namespace HedonismBlog.Controllers
{
    public class Account : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
