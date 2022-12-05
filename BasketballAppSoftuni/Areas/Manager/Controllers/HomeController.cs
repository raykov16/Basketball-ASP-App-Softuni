using Microsoft.AspNetCore.Mvc;

namespace BasketballAppSoftuni.Areas.Manager.Controllers
{
    public class HomeController : ManagerBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
