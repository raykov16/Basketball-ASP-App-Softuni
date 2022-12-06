using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Models.MatchViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAppSoftuni.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMatchService _matchService;
        public HomeController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public async Task<IActionResult> Index()
        {
            var allUpcomingMatches = await _matchService.GetMatchesWithTicketsAsync();

            var top5Upcoming = allUpcomingMatches
                .Take(5)
                .Select(m => new MatchBuyTicketViewModel
            {
                ArenaId = m.ArenaId,
                AwayTeamId = m.AwayTeamId,
                AwayTeamLogo = m.AwayTeamLogo,
                AwayTeamName = m.AwayTeamName,
                Date = m.Date,
                HomeTeamId = m.HomeTeamId,
                HomeTeamLogo = m.HomeTeamLogo,
                HomeTeamName = m.HomeTeamName,
                MatchId = m.MatchId
            });

            return View(top5Upcoming);
        }
    }
}