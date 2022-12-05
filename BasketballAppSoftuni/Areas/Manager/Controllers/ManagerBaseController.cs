using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAppSoftuni.Areas.Manager.Controllers
{
    [Area("Manager")]
   // [Authorize(Roles = "Manager")]
    public class ManagerBaseController : Controller
    {

    }
}
