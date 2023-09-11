using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
