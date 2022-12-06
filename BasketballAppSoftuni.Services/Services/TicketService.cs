using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Data;
using BasketballAppSoftuni.Data.Entities;
using BasketballAppSoftuni.DTOs.TicketDTOs;
using Microsoft.EntityFrameworkCore;

namespace BasketballAppSoftuni.Services
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationDbContext _context;
        public TicketService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TicketDTO> CreateTicketAsync(int matchId)
        {
            var match = await _context.Matches
                .Include(m => m.Arena)
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Where(m => m.Id == matchId)
                .FirstOrDefaultAsync();

            var ticket = new TicketDTO()
            {
                MatchId = matchId,
                ArenaName = match.Arena.Name,
                ArenaLocation = match.Arena.Location,
                AwayTeamLogo = match.AwayTeam.LogoURL,
                HomeTeamLogo = match.HomeTeam.LogoURL,
                TicketsAvailable = match.TicketsAvailable,
                PricePerTicket = match.TicketPrice
            };

            return ticket;
        }

        public async Task BuyTicketsAsync(string userId, int matchId, int ticketsBought)
        {
            var user = await _context.Users
                .Include(u => u.UserMatches)
                .Where(u => u.Id == userId)
                .SingleOrDefaultAsync();

            var match = await _context.Matches.FindAsync(matchId);

            if (match != null && match.TicketsAvailable > 0)
            {
                match.TicketsAvailable -= ticketsBought;

                if (!user.UserMatches.Any(um => um.MatchId == matchId))
                {
                    user.UserMatches.Add(new UserMatch()
                    {
                        User = user,
                        UserId = userId,
                        Match = match,
                        MatchId = matchId
                    });
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
