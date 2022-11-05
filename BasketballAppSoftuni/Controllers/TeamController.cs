using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Models.TeamsModels;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAppSoftuni.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;
        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<IActionResult> AllTeams()
        {
            List<TeamShortInfoViewModel> models = await _teamService.GetAllAsync();

            return View(models);
        }

        public async Task<IActionResult> TeamDetails(int teamId)
        {
            TeamDetailsViewModel model = await _teamService.Get(teamId);

            return View(model);
        }
    }
}
