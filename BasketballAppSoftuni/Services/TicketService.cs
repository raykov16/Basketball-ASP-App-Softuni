using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Data;
using BasketballAppSoftuni.Data.Entities;
using BasketballAppSoftuni.Models.TicketViewModels;
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

		public async Task<TicketViewModel> CreateTicketAsync(int matchId)
		{
			var match = await _context.Matches
				.Include(m => m.Arena)
				.Include(m => m.HomeTeam)
				.Include(m => m.AwayTeam)
				.Where(m => m.Id == matchId)
				.FirstOrDefaultAsync();

			var ticket = new TicketViewModel()
			{
				MatchId = matchId,
				ArenaName = match.Arena.Name,
				ArenaLocation = match.Arena.Location,
				AwayTeamLogo = match.AwayTeam.LogoURL,
				HomeTeamLogo = match.HomeTeam.LogoURL,
				TicketsAvailable = match.TicketsAvailable,
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

			await _context.SaveChangesAsync();
		}
	}
}
