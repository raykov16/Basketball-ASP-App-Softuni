using BasketballAppSoftuni.DTOs.PlayerDTOs;

namespace BasketballAppSoftuni.Contracts
{
    public interface IPlayerService
    {
        public Task<List<PlayerTeamAndPositionDTO>> GetAllAsync();

        public Task<PlayerFullInfoDTO> GetAsync(int playerId);
    }
}
