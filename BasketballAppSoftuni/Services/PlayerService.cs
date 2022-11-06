using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Data;
using BasketballAppSoftuni.Models.PlayerModels;
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

        public async Task<List<PlayerTeamAndPositionViewModel>> GetAllAsync()
        {
            return await _context.Players
                .Select(p => new PlayerTeamAndPositionViewModel
                {
                    Id = p.Id,
                    FullName = p.FirstName + " " + p.LastName,
                    PictureURL = p.PictureURL,
                    Position = p.Position,
                    TeamName = p.Team.Name
                })
                .ToListAsync();
        }
    }
}
