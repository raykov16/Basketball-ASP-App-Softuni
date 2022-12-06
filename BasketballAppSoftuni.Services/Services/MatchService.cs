using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Data;
using BasketballAppSoftuni.DTOs.MatchDTOs;
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

        public async Task<List<MatchTableDTO>> AllMatchesAsync()
        {
            return await _context.Matches
                .OrderBy(m => m.GameDate)
                .Select(m => new MatchTableDTO()
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

        public async Task<List<MatchBuyTicketDTO>> GetMatchesWithTicketsAsync()
        {
            return await _context.Matches
                .Where(m => m.TicketsAvailable > 0)
                .OrderBy(m => m.GameDate)
                .Select(m => new MatchBuyTicketDTO()
                {
                    MatchId = m.Id,
                    ArenaId = m.HomeTeam.ArenaId,
                    Date = m.GameDate.ToString("dddd, dd MMMM yyyy hh:mm tt"),
                    HomeTeamId = m.HomeTeamId,
                    AwayTeamId = m.AwayTeamId,
                    HomeTeamLogo = m.HomeTeam.LogoURL,
                    AwayTeamLogo = m.AwayTeam.LogoURL,
                    HomeTeamName = m.HomeTeam.Name,
                    AwayTeamName = m.AwayTeam.Name,
                })
                .ToListAsync();
        }

        public async Task<List<MyMatchesDTO>> GetMyMatchesAsync(string userId)
        {
            var user = await _context.Users
            .Include(u => u.UserMatches)
            .ThenInclude(um => um.Match)
            .ThenInclude(m => m.HomeTeam)
            .ThenInclude(ht => ht.Arena)
            .Include(u => u.UserMatches)
            .ThenInclude(um => um.Match)
            .ThenInclude(m => m.AwayTeam)
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();

            var myMatchesModels = user.UserMatches
           .Select(um => new MyMatchesDTO()
           {
               MatchDate = um.Match.GameDate.ToString("dddd, dd MMMM yyyy hh:mm tt"),
               ArenaName = um.Match.HomeTeam.Arena.Name,
               ArenaLocation = um.Match.HomeTeam.Arena.Location,
               AwayTeamLogo = um.Match.AwayTeam.LogoURL,
               AwayTeamName=um.Match.AwayTeam.Name,
               AwayTeamPoints = um.Match.AwayTeamPoints,
               HomeTeamLogo = um.Match.HomeTeam.LogoURL,
               HomeTeamName = um.Match.HomeTeam.Name,
               HomeTeamPoints = um.Match.HomeTeamPoints
           })
           .OrderBy(m => m.MatchDate)
           .ToList();

            return myMatchesModels;
        }
    }
}
