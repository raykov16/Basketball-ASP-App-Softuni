using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Data;
using BasketballAppSoftuni.DTOs.PlayerDTOs;
using Microsoft.EntityFrameworkCore;

namespace BasketballAppSoftuni.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly ApplicationDbContext _context;
        public PlayerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PlayerTeamAndPositionDTO>> GetAllAsync()
        {
            return await _context.Players
                .Select(p => new PlayerTeamAndPositionDTO
                {
                    Id = p.Id,
                    FullName = p.FirstName + " " + p.LastName,
                    PictureURL = p.PictureURL,
                    Position = p.Position,
                    TeamName = p.Team.Name
                })
                .ToListAsync();
        }

        public async Task<PlayerFullInfoDTO> GetAsync(int playerId)
        {
            return await _context.Players
                .Where(p => p.Id == playerId)
                .Select(p => new PlayerFullInfoDTO
                {
                    Age = p.Age,
                    TeamId = p.Team.Id,
                    Height = p.Height,
                    Salary = p.Salary,
                    Position = p.Position,
                    FullName = p.FirstName + " " + p.LastName,
                    PictureURL = p.PictureURL,
                    TeamLogoUrl = p.Team.LogoURL,
                    PointsPerGame = p.PointsPerGame,
                    AssistsPerGame = p.AssistsPerGame,
                    ReboundsPerGame = p.ReboundsPerGame
                })
                .SingleAsync();
        }
    }
}
