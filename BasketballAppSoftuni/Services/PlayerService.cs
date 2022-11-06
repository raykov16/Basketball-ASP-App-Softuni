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

        public async Task<List<PlayerShortInfoViewModel>> GetAllAsync()
        {
            return await _context.Players
                .Select(p => new PlayerShortInfoViewModel
                {
                    Id = p.Id,
                    FullName = p.FirstName + " " + p.LastName,
                    PictureURL = p.PictureURL
                })
                .ToListAsync();
        }
    }
}
