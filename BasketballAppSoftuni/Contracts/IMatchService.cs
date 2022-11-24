using BasketballAppSoftuni.Models.MatchViewModels;

namespace BasketballAppSoftuni.Contracts
{
    public interface IMatchService
    {
        public Task<List<MatchTableViewModel>> AllMatchesAsync();
        public Task<List<MatchBuyTicketViewModel>> GetMatchesWithTicketsAsync();
        public Task<List<MyMatchesViewModel>> GetMyMatchesAsync(string userId);
    }
}
