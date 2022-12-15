using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.DTOs.TeamDTOs;
using BasketballAppSoftuni.Models.TeamsModels;
using Microsoft.AspNetCore.Mvc;
using BasketballAppSoftuni.Data.Entities;
using BasketballAppSoftuni.Models.PlayerModels;
using BasketballAppSoftuni.Web.Constants;
using Microsoft.Extensions.Caching.Memory;

namespace BasketballAppSoftuni.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;
        private readonly IMemoryCache _cache;
        public TeamController(ITeamService teamService, IMemoryCache cache)
        {
            _teamService = teamService;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> AllTeams()
        {
            try
            {
                var models = _cache.Get<IEnumerable<TeamShortInfoViewModel>>(CacheKeys.AllTeamsKey);

                if (models == null)
                {
                    var dtos = await _teamService.GetAllAsync();
                    models = MapAllTeamsModels(dtos);

                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(40));

                    _cache.Set(CacheKeys.AllTeamsKey, models, cacheOptions);
                }

                return View(models);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { message = ErrorMessages.AllTeamsError });
            }
        }

        public async Task<IActionResult> TeamDetails(int teamId)
        {
            try
            {
                var dto = await _teamService.GetAsync(teamId);

                var model = MapTeamDetailsModel(dto);

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { message = ErrorMessages.TeamError });
            }
        }

        private static IEnumerable<TeamShortInfoViewModel> MapAllTeamsModels(List<TeamShortInfoDTO> dtos)
        {
            return dtos.Select(d => new TeamShortInfoViewModel
            {
                Id = d.Id,
                LogoURL = d.LogoURL,
                Name = d.Name
            });
        }
        private static TeamDetailsViewModel MapTeamDetailsModel(TeamDetailsDTO dto)
        {
            return new TeamDetailsViewModel
            {
                HomeTown = dto.HomeTown,
                Id = dto.Id,
                LogoURL = dto.LogoURL,
                Loses = dto.Loses,
                Name = dto.Name,
                Wins = dto.Wins,
                Arena = new Arena
                {
                    Id = dto.Arena.Id,
                    Location = dto.Arena.Location,
                    Name = dto.Arena.Name,
                    PictureURL = dto.Arena.PictureURL,
                    Seats = dto.Arena.Seats
                },
                Players = dto.Players.Select(p => new PlayerShortInfoViewModel
                {
                    FullName = p.FullName,
                    Id = p.Id,
                    PictureURL = p.PictureURL
                })
            };
        }

    }
}
