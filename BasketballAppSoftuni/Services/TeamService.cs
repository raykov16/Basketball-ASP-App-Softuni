using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Data;
using BasketballAppSoftuni.Models.TeamsModels;
using BasketballAppSoftuni.Models.PlayerModels;
using Microsoft.EntityFrameworkCore;

namespace BasketballAppSoftuni.Services
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext _context;

        public TeamService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TeamShortInfoViewModel>> GetAllAsync()
        {
            return await _context.Teams.Select(t => new TeamShortInfoViewModel
            {
                Id = t.Id,
                LogoURL = t.LogoURL,
                Name = t.Name
            })
                .ToListAsync();
        }

        public async Task<TeamDetailsViewModel> GetAsync(int teamId)
        {
            return await _context.Teams
            .Where(t => t.Id == teamId)
            .Select(t => new TeamDetailsViewModel
            {
                Id = t.Id,
                Name = t.Name,
                Arena = t.Arena,
                HomeTown = t.HomeTown,
                LogoURL = t.LogoURL,
                Loses = t.Loses,
                Wins = t.Wins,
                Players = _context.Players
                .Where(p => p.TeamId == t.Id)
                .Select(p => new PlayerShortInfoViewModel
                {
                    FullName = p.FirstName + " " + p.LastName,
                    Id = p.Id,
                    PictureURL = p.PictureURL
                })
                .ToList()
            })
            .SingleAsync();
        }
    }
}
