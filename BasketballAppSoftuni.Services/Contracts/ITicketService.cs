using BasketballAppSoftuni.DTOs.TicketDTOs;

namespace BasketballAppSoftuni.Contracts
{
	public interface ITicketService
	{
        public Task<TicketDTO> CreateTicketAsync(int matchId);
        public Task BuyTicketsAsync(string userId, int matchId, int ticketsBought);
    }
}
