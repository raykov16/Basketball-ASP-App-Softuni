using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.DTOs.PlayerDTOs;
using BasketballAppSoftuni.Models.PlayerModels;
using Microsoft.AspNetCore.Mvc;

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
            var dtos = await _playerService.GetAllAsync();
            var models = MapAllPlayersModels(dtos);

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
            var d = await _playerService.GetAsync(playerId);

            var model = MapPlayerModel(d);

            return View(model);
        }

        private static IEnumerable<PlayerTeamAndPositionViewModel> MapAllPlayersModels(List<PlayerTeamAndPositionDTO> dtos)
        {
            return dtos.Select(d => new PlayerTeamAndPositionViewModel
            {
                FullName = d.FullName,
                Id = d.Id,
                PictureURL = d.PictureURL,
                Position = d.Position,
                TeamName = d.TeamName
            });
        }
        private static PlayerFullInfoViewModel MapPlayerModel(PlayerFullInfoDTO dto)
        {
            return new PlayerFullInfoViewModel
            {
                Age = dto.Age,
                AssistsPerGame = dto.AssistsPerGame,
                FullName = dto.FullName,
                Height = dto.Height,
                PictureURL = dto.PictureURL,
                PointsPerGame = dto.PointsPerGame,
                Position = dto.Position,
                ReboundsPerGame = dto.ReboundsPerGame,
                Salary = dto.Salary,
                TeamId = dto.TeamId,
                TeamLogoUrl = dto.TeamLogoUrl
            };
        }
    }
}
