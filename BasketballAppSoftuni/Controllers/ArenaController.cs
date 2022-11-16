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
            List<ArenaDetailsViewModel> models = await _arenaService.GetAllAsync();
            return View(models);
        }

        public async Task<IActionResult> ArenaDetails(int arenaId)
        {
            ArenaDetailsViewModel model = await _arenaService.GetAsync(arenaId);
            return View(model);
        }
    }
}
