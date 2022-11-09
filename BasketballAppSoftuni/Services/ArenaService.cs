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

        public async Task<List<ArenaShortInfoViewModel>> GetAllAsync()
        {
            return await _context.Arenas
                .Select(a => new ArenaShortInfoViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    PictureURL = a.PictureURL
                })
                .ToListAsync();
        }
    }
}
