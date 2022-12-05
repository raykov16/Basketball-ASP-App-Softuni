using BasketballAppSoftuni.Areas.Manager.Models;
using BasketballAppSoftuni.Data.Entities;

namespace BasketballAppSoftuni.Contracts
{
    public interface IManagerService
    {
        public Task<IEnumerable<TeamShortInfoModel>> GetAllTeamNamesAsync();
        public Task AddMatchAsync(ScheduleMatchModel model);
        public Task<IEnumerable<RescheduleMatchModel>> GetUnplayedMatchesAsync();
        public Task<IEnumerable<UpdateMatchResultModel>> GetMatchesForUpdateAsync();
        public Task RescheduleMatchAsync(int matchId, DateTime date);
        public Task UpdateMatchScoreAsync(int matchId, int homeTeamPoints, int awayTeamPoitns);
        public Task RemoveMatch(int matchId);
    }
}
