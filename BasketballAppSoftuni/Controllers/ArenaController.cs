using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Models.ArenaViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAppSoftuni.Controllers
{
    public class ArenaController : Controller
    {
        private readonly IArenaService _arenaService;
        public ArenaController(IArenaService arenaService)
        {
            _arenaService = arenaService;
        }

        public async Task<IActionResult> AllArenas()
        {
            List<ArenaShortInfoViewModel> models = await _arenaService.GetAllAsync();
            return View(models);
        }
    }
}
