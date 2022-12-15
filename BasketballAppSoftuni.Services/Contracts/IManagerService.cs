using BasketballAppSoftuni.DTOs.ManagerAreaDTOs;

namespace BasketballAppSoftuni.Contracts
{
    public interface IManagerService
    {
        public Task<IEnumerable<TeamShortInfoDTO>> GetAllTeamNamesAsync();
        public Task AddMatchAsync(ScheduleMatchDTO model);
        public Task<IEnumerable<RescheduleMatchDTO>> GetUnplayedMatchesAsync();
        public Task<IEnumerable<UpdateMatchResultDTO>> GetMatchesForUpdateAsync();
        public Task RescheduleMatchAsync(int matchId, DateTime date);
        public Task UpdateMatchScoreAsync(int matchId, int homeTeamPoints, int awayTeamPoitns);
        public Task RemoveMatchAsync(int matchId);
    }
}
