using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Models.MatchViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

            if(teamId > 0)
            {
                matchModels = matchModels
                    .Where(m =>  m.HomeTeamId == teamId || m.AwayTeamId == teamId)
                    .ToList();
            }

            var teamModels = await _teamService.GetAllAsync();

            var model = new AllMatchesViewModel()
            {
                matchModels = matchModels,
                teamModels = teamModels
            };
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> MatchesWithTickets(int teamId)
        {
            var matchModels = await _matchService.GetMatchesWithTicketsAsync();

            if (teamId > 0)
            {
                matchModels = matchModels
                    .Where(m => m.HomeTeamId == teamId || m.AwayTeamId == teamId)
                    .ToList();
            }

            var teamModels = await _teamService.GetAllAsync();

            var model = new MatchesWithTicketsViewModel()
            {
                matchModels = matchModels,
                teamModels = teamModels
            };

            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> MyMatches()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var models = await _matchService.GetMyMatchesAsync(userId);

            return View(models);
        }
    }
}
