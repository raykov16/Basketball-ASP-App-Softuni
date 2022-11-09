using BasketballAppSoftuni.Models.ArenaViewModels;

namespace BasketballAppSoftuni.Contracts
{
    public interface IArenaService
    {
        public Task<List<ArenaShortInfoViewModel>> GetAllAsync();
    }
}
