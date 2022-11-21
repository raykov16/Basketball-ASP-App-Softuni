using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Models.MatchViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAppSoftuni.Controllers
{
    public class MatchController : Controller
    {
        private readonly IMatchService _matchService;
        private readonly ITeamService _teamService;

        public MatchController(IMatchService matchService,ITeamService teamService)
        {
            _matchService = matchService;
            _teamService = teamService;
        }

        public async Task<IActionResult> AllMatches(int teamId)
        {
            var matchModels = await _matchService.AllMatchesAsync();
            var teamModels = await _teamService.GetAllAsync();

            if(teamId > 0)
            {
                matchModels = matchModels
                    .Where(m =>  m.HomeTeamId == teamId || m.AwayTeamId == teamId)
                    .ToList();
            }

            var model = new AllMatchesViewModel()
            {
                matchModels = matchModels,
                teamModels = teamModels
            };
            return View(model);
        }
    }
}
