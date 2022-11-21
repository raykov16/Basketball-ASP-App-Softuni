using BasketballAppSoftuni.Models.MatchViewModels;

namespace BasketballAppSoftuni.Contracts
{
    public interface IMatchService
    {
        public Task<List<MatchTableViewModel>> AllMatchesAsync();
    }
}
