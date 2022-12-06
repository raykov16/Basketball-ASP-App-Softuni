using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Data;
using BasketballAppSoftuni.DTOs.ArenaDTOs;
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

        public async Task<List<ArenaDetailsDTO>> GetAllAsync()
        {
            return await _context.Arenas
                .Select(a => new ArenaDetailsDTO
                {
                    Name = a.Name,
                    PictureURL = a.PictureURL,
                    Location = a.Location,
                    Seats = a.Seats
                })
                .ToListAsync();
        }

        public async Task<ArenaDetailsDTO> GetAsync(int arenaId)
        {
            return await _context.Arenas
                .Where(a => a.Id == arenaId)
                .Select(a => new ArenaDetailsDTO
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
