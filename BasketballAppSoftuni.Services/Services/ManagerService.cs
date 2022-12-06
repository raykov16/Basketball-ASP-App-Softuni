using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Data;
using BasketballAppSoftuni.Data.Entities;
using BasketballAppSoftuni.DTOs.ManagerAreaDTOs;
using Microsoft.EntityFrameworkCore;

namespace BasketballAppSoftuni.Services
{
    public class ManagerService : IManagerService
    {
        private readonly ApplicationDbContext _context;

        public ManagerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddMatchAsync(ScheduleMatchDTO model)
        {
            Team homeTeam = await _context.Teams
                .Include(t => t.Arena)
                .Where(t => t.Id == model.HomeTeamId)
                .SingleAsync();

            Team awayTeam = await _context.Teams
                .Where(t => t.Id == model.AwayTeamId)
                .SingleAsync();

            Match match = new Match()
            {
                AwayTeamId = model.AwayTeamId,
                HomeTeamId = homeTeam.Id,
                Arena = homeTeam.Arena,
                ArenaId = homeTeam.ArenaId,
                AwayTeam = awayTeam,
                TicketsAvailable = homeTeam.Arena.Seats,
                GameDate = model.MatchDate,
                HomeTeam = homeTeam,
                TicketPrice = model.TicketPrice,
            };

            _context.Matches.Add(match);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TeamShortInfoDTO>> GetAllTeamNamesAsync()
        {
            return await _context.Teams
                .Select(t => new TeamShortInfoDTO()
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<RescheduleMatchDTO>> GetUnplayedMatchesAsync()
        {
            return await _context.Matches.Where(m => m.HomeTeamPoints == null && m.GameDate > DateTime.Now)
                .Select(m => new RescheduleMatchDTO
                {
                    MatchId = m.Id,
                    MatchDate = m.GameDate,
                    HomeTeamName = m.HomeTeam.Name,
                    AwayTeamName = m.AwayTeam.Name,
                    HomeTeamLogo = m.HomeTeam.LogoURL,
                    AwayTeamLogo = m.AwayTeam.LogoURL
                })
                .OrderBy(m => m.MatchDate)
                .ToListAsync();
        }

        public async Task RescheduleMatchAsync(int matchId, DateTime date)
        {
            var match = await _context.Matches.FindAsync(matchId);

            match.GameDate = date;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UpdateMatchResultDTO>> GetMatchesForUpdateAsync()
        {
            return await _context.Matches
                .Where(m => m.HomeTeamPoints == null && m.GameDate < DateTime.Now)
                .Select(m => new UpdateMatchResultDTO
                {
                    AwayTeamName = m.AwayTeam.Name,
                    MatchDate = m.GameDate.ToString("dddd, dd MMMM yyyy hh:mm tt"),
                    HomeTeamName = m.HomeTeam.Name,
                    MatchId = m.Id
                })
                .ToListAsync();
        }

        public async Task UpdateMatchScoreAsync(int matchId,int homeTeamPoints, int awayTeamPoitns)
        {
            var match = _context.Matches.Find(matchId);

            match.HomeTeamPoints = homeTeamPoints;
            match.AwayTeamPoints = awayTeamPoitns;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveMatch(int matchId)
        {
            var match = _context.Matches.Find(matchId);

            _context.Matches.Remove(match);

            await _context.SaveChangesAsync();
        }
    }
}
