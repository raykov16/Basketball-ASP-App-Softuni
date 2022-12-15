using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Data;
using Microsoft.EntityFrameworkCore;
using BasketballAppSoftuni.DTOs.TeamDTOs;
using BasketballAppSoftuni.DTOs.PlayerDTOs;

namespace BasketballAppSoftuni.Services
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext _context;

        public TeamService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TeamShortInfoDTO>> GetAllAsync()
        {
            return await _context.Teams.Select(t => new TeamShortInfoDTO
            {
                Id = t.Id,
                LogoURL = t.LogoURL,
                Name = t.Name
            })
                .ToListAsync();
        }

        public async Task<TeamDetailsDTO> GetAsync(int teamId)
        {
            var players = await _context.Players
                .Where(p => p.TeamId == teamId)
                .Select(p => new PlayerShortInfoDTO
                {
                    FullName = p.FirstName + " " + p.LastName,
                    Id = p.Id,
                    PictureURL = p.PictureURL
                })
                .ToListAsync();

            return await _context.Teams
            .Where(t => t.Id == teamId)
            .Select(t => new TeamDetailsDTO
            {
                Id = t.Id,
                Name = t.Name,
                Arena = t.Arena,
                HomeTown = t.HomeTown,
                LogoURL = t.LogoURL,
                Loses = t.Loses,
                Wins = t.Wins,
                Players = players
            })
            .SingleAsync();
        }
    }
}
