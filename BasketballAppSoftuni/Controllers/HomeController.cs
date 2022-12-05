using BasketballAppSoftuni.Contracts;
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
            var top5Upcoming = allUpcomingMatches.Take(5);
            return View(top5Upcoming);
        }
    }
}