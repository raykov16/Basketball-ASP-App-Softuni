using BasketballAppSoftuni.Models.TicketViewModels;

namespace BasketballAppSoftuni.Contracts
{
	public interface ITicketService
	{
        public Task<TicketViewModel> CreateTicketAsync(int matchId);
        public Task BuyTicketsAsync(string userId, int matchId, int ticketsBought);
    }
}
