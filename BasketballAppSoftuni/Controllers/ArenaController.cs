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
            var dtos = await _arenaService.GetAllAsync();

            IEnumerable<ArenaDetailsViewModel> models = dtos
                .Select(d => new ArenaDetailsViewModel
                {
                    Location = d.Location,
                    Name = d.Name,
                    PictureURL = d.PictureURL,
                    Seats = d.Seats
                });

            return View(models);
        }

        public async Task<IActionResult> ArenaDetails(int arenaId)
        {
            var dto = await _arenaService.GetAsync(arenaId);

            var model = new ArenaDetailsViewModel
            {
                Location = dto.Location,
                Name = dto.Name,
                PictureURL = dto.PictureURL,
                Seats = dto.Seats
            };

            return View(model);
        }
    }
}
