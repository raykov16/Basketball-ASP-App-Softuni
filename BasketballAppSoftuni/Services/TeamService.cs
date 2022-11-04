using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Data;
using BasketballAppSoftuni.Models.TeamsModels;
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
            
        
    }
}
