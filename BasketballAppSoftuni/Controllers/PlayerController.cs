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
        public async Task<IActionResult> AllPlayers(string nameSearchCriteria)
        {
            List<PlayerTeamAndPositionViewModel> models = await _playerService.GetAllAsync();
            //TODO novo entity za tva ^ shte sudurja full name ,team name , snimka i position 
            if (nameSearchCriteria != null)
            {
                nameSearchCriteria = nameSearchCriteria.ToLower();
                models = models
                    .Where(p => p.FullName.ToLower().Contains(nameSearchCriteria))
                    .ToList();
                //if (customerDTOs.Count == 0)
                //{
                //    model.ErrorMessage = string.Format(Strings.NoCustomerFoundByCriteria, nameSearchCriteria);
                //    return View(model);
                //}
            }

            return View(models);
        }
    }
}
