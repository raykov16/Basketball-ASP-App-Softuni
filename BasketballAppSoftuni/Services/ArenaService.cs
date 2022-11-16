using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Data;
using BasketballAppSoftuni.Models.ArenaViewModels;
using Microsoft.EntityFrameworkCore;

namespace BasketballAppSoftuni.Services
{
    public class ArenaService : IArenaService
    {
        private readonly ApplicationDbContext _context;

        public ArenaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ArenaDetailsViewModel>> GetAllAsync()
        {
            return await _context.Arenas
                .Select(a => new ArenaDetailsViewModel
                {
                    Name = a.Name,
                    PictureURL = a.PictureURL,
                    Location = a.Location,
                    Seats = a.Seats
                })
                .ToListAsync();
        }

        public async Task<ArenaDetailsViewModel> GetAsync(int arenaId)
        {
            return await _context.Arenas
                .Where(a => a.Id == arenaId)
                .Select(a => new ArenaDetailsViewModel
                {
                    Name = a.Name,
                    Seats = a.Seats,
                    Location = a.Location,
                    PictureURL = a.PictureURL
                })
                .SingleAsync();
        }
    }
}
