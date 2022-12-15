using BasketballAppSoftuni.DTOs.MatchDTOs;

namespace BasketballAppSoftuni.Contracts
{
    public interface IMatchService
    {
        public Task<List<MatchTableDTO>> GetAllMatchesAsync();
        public Task<List<MatchBuyTicketDTO>> GetMatchesWithTicketsAsync();
        public Task<List<MyMatchesDTO>> GetMyMatchesAsync(string userId);
    }
}
