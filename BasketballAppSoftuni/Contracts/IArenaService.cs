using BasketballAppSoftuni.Models.ArenaViewModels;

namespace BasketballAppSoftuni.Contracts
{
    public interface IArenaService
    {
        public Task<List<ArenaDetailsViewModel>> GetAllAsync();
        public Task<ArenaDetailsViewModel> GetAsync(int arenaId);
    }
}
