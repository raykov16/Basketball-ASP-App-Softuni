﻿using BasketballAppSoftuni.Models.PlayerModels;

namespace BasketballAppSoftuni.Contracts
{
    public interface IPlayerService
    {
        public Task<List<PlayerTeamAndPositionViewModel>> GetAllAsync();
    }
}