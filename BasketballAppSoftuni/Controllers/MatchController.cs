using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.DTOs.MatchDTOs;
using BasketballAppSoftuni.DTOs.TeamDTOs;
using BasketballAppSoftuni.Models.MatchViewModels;
using BasketballAppSoftuni.Models.TeamsModels;
using BasketballAppSoftuni.Web.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace BasketballAppSoftuni.Controllers
{
    public class MatchController : Controller
    {
        private readonly IMatchService _matchService;
        private readonly ITeamService _teamService;
        private readonly IMemoryCache _cache;

        public MatchController(IMatchService matchService, ITeamService teamService, IMemoryCache cache)
        {
            _matchService = matchService;
            _teamService = teamService;
            _cache = cache;
        }

        public async Task<IActionResult> AllMatches(int teamId)
        {
            try
            {
                var matchModels = _cache.Get<IEnumerable<MatchTableViewModel>>(CacheKeys.AllMatchesKey);

                if (matchModels == null)
                {
                    var matchDTOs = await _matchService.GetAllMatchesAsync();
                    matchModels = MapMatchTableModels(matchDTOs);

                    var cacheOptions = new MemoryCacheEntryOptions()
                      .SetSlidingExpiration(TimeSpan.FromSeconds(40));

                    _cache.Set(CacheKeys.AllMatchesKey, matchModels, cacheOptions);
                }

                if (teamId > 0)
                {
                    matchModels = matchModels
                        .Where(m => m.HomeTeamId == teamId || m.AwayTeamId == teamId)
                        .ToList();
                }

                var teamModels = _cache.Get<IEnumerable<TeamShortInfoViewModel>>(CacheKeys.AllTeamsKey);

                if (teamModels == null)
                {
                    var dtos = await _teamService.GetAllAsync();
                    teamModels = MapTeamModels(dtos);

                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(40));

                    _cache.Set(CacheKeys.AllTeamsKey, teamModels, cacheOptions);
                }

                var model = new AllMatchesViewModel()
                {
                    matchModels = matchModels,
                    teamModels = teamModels
                };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { message = ErrorMessages.AllMatchesError });
            }
        }

        [Authorize]
        public async Task<IActionResult> MatchesWithTickets(int teamId)
        {
            try
            {
                var matchDTOs = await _matchService.GetMatchesWithTicketsAsync();
                var matchModels = MapMatchBuyTicketModels(matchDTOs);

                if (teamId > 0)
                {
                    matchModels = matchModels
                        .Where(m => m.HomeTeamId == teamId || m.AwayTeamId == teamId)
                        .ToList();
                }

                var teamModels = _cache.Get<IEnumerable<TeamShortInfoViewModel>>(CacheKeys.AllTeamsKey);

                if (teamModels == null)
                {
                    var dtos = await _teamService.GetAllAsync();
                    teamModels = MapTeamModels(dtos);

                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(40));

                    _cache.Set(CacheKeys.AllTeamsKey, teamModels, cacheOptions);
                }

                var model = new MatchesWithTicketsViewModel()
                {
                    matchModels = matchModels,
                    teamModels = teamModels
                };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { message = ErrorMessages.AllMatchesError });
            }
        }

        [Authorize]
        public async Task<IActionResult> MyMatches()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var dtos = await _matchService.GetMyMatchesAsync(userId);

                var models = MapMyMatchesModels(dtos);

                return View(models);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { message = ErrorMessages.AllMatchesError });
            }
        }

        private static IEnumerable<TeamShortInfoViewModel> MapTeamModels(List<TeamShortInfoDTO> teamDtos)
        {
            return teamDtos.Select(t => new TeamShortInfoViewModel
            {
                Id = t.Id,
                LogoURL = t.LogoURL,
                Name = t.Name
            });
        }
        private static IEnumerable<MatchTableViewModel> MapMatchTableModels(List<MatchTableDTO> matchDTOs)
        {
            return matchDTOs.Select(d => new MatchTableViewModel
            {
                AwayTeamId = d.AwayTeamId,
                AwayTeamLogo = d.AwayTeamLogo,
                AwayTeamName = d.AwayTeamName,
                AwayTeamPoints = d.AwayTeamPoints,
                HomeTeamId = d.HomeTeamId,
                HomeTeamLogo = d.HomeTeamLogo,
                HomeTeamName = d.HomeTeamName,
                HomeTeamPoints = d.HomeTeamPoints
            });
        }
        private static IEnumerable<MatchBuyTicketViewModel> MapMatchBuyTicketModels(List<MatchBuyTicketDTO> matchDTOs)
        {
            return matchDTOs.Select(d => new MatchBuyTicketViewModel
            {
                ArenaId = d.ArenaId,
                AwayTeamId = d.AwayTeamId,
                AwayTeamLogo = d.AwayTeamLogo,
                AwayTeamName = d.AwayTeamName,
                Date = d.Date,
                HomeTeamId = d.HomeTeamId,
                HomeTeamLogo = d.HomeTeamLogo,
                HomeTeamName = d.HomeTeamName,
                MatchId = d.MatchId
            });
        }
        private static IEnumerable<MyMatchesViewModel> MapMyMatchesModels(List<MyMatchesDTO> dtos)
        {
            return dtos.Select(d => new MyMatchesViewModel
            {
                ArenaLocation = d.ArenaLocation,
                ArenaName = d.ArenaName,
                AwayTeamLogo = d.AwayTeamLogo,
                AwayTeamName = d.AwayTeamName,
                AwayTeamPoints = d.AwayTeamPoints,
                HomeTeamLogo = d.HomeTeamLogo,
                HomeTeamName = d.HomeTeamName,
                HomeTeamPoints = d.HomeTeamPoints,
                MatchDate = d.MatchDate
            });
        }
    }
}
