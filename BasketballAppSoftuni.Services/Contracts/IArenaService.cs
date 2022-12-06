using BasketballAppSoftuni.DTOs.ArenaDTOs;

namespace BasketballAppSoftuni.Contracts
{
    public interface IArenaService
    {
        public Task<List<ArenaDetailsDTO>> GetAllAsync();
        public Task<ArenaDetailsDTO> GetAsync(int arenaId);
    }
}
