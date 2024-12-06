using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalMMNP.Controllers.Admin
{
    [Authorize]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
