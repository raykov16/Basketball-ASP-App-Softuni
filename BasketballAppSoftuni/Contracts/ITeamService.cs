using BasketballAppSoftuni.Models.TeamsModels;

namespace BasketballAppSoftuni.Contracts
{
    public interface ITeamService
    {
        public Task<List<TeamShortInfoViewModel>> GetAllAsync();
        public Task<TeamDetailsViewModel> Get(int teamId);
    }
}
