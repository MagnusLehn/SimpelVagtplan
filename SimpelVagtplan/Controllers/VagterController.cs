using Microsoft.AspNetCore.Mvc;

namespace SimpelVagtplan.Controllers
{
    public class VagterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
