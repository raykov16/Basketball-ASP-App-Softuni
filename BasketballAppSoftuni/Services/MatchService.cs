using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Data;
using BasketballAppSoftuni.Models.MatchViewModels;
using Microsoft.EntityFrameworkCore;

namespace BasketballAppSoftuni.Services
{
    public class MatchService : IMatchService
    {
        private readonly ApplicationDbContext _context;
        public MatchService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MatchTableViewModel>> AllMatchesAsync()
        {
            return await _context.Matches
                .OrderBy(m => m.GameDate)
                .Select(m => new MatchTableViewModel()
                {
                    HomeTeamId = m.HomeTeamId,
                    AwayTeamId = m.AwayTeamId,
                    HomeTeamLogo = m.HomeTeam.LogoURL,
                    AwayTeamLogo = m.AwayTeam.LogoURL,
                    HomeTeamName = m.HomeTeam.Name,
                    AwayTeamName = m.AwayTeam.Name,
                    HomeTeamPoints = m.HomeTeamPoints,
                    AwayTeamPoints = m.AwayTeamPoints,
                })
                .ToListAsync();
        }
    }
}
