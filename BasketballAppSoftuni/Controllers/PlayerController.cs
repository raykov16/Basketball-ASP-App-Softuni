using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Models.PlayerModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace BasketballAppSoftuni.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;
        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        public async Task<IActionResult> AllPlayers(string nameSearchCriteria,string position)
        {
            List<PlayerTeamAndPositionViewModel> models = await _playerService.GetAllAsync();

            if (nameSearchCriteria != null)
            {
                nameSearchCriteria = nameSearchCriteria.ToLower();
                models = models
                    .Where(p => p.FullName.ToLower().StartsWith(nameSearchCriteria))
                    .ToList();
            }

            if (position != null)
            {
                position = position.ToLower();
                models = models.
                    Where(p => p.Position.ToLower() == position)
                    .ToList();
            }

            return View(models);
        }

        public async Task<IActionResult> PlayerDetails(int playerId)
        {
            PlayerFullInfoViewModel model = await _playerService.GetAsync(playerId);
            return View(model);
        }
    }
}
