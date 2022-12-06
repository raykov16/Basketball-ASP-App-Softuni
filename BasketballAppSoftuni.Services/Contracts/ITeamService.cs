using BasketballAppSoftuni.DTOs.TeamDTOs;

namespace BasketballAppSoftuni.Contracts
{
    public interface ITeamService
    {
        public Task<List<TeamShortInfoDTO>> GetAllAsync();
        public Task<TeamDetailsDTO> GetAsync(int teamId);
    }
}
