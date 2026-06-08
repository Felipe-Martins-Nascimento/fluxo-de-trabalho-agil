using Microsoft.AspNetCore.Mvc;

namespace FluxoDeTrabalhoAgil.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}