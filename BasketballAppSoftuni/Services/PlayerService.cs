﻿using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Data;
using BasketballAppSoftuni.Models.PlayerModels;
using Microsoft.EntityFrameworkCore;

namespace BasketballAppSoftuni.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly ApplicationDbContext _context;
        public PlayerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PlayerTeamAndPositionViewModel>> GetAllAsync()
        {
            return await _context.Players
                .Select(p => new PlayerTeamAndPositionViewModel
                {
                    Id = p.Id,
                    FullName = p.FirstName + " " + p.LastName,
                    PictureURL = p.PictureURL,
                    Position = p.Position,
                    TeamName = p.Team.Name
                })
                .ToListAsync();
        }

        public async Task<PlayerFullInfoViewModel> GetAsync(int playerId)
        {
            return await _context.Players
                .Where(p => p.Id == playerId)
                .Select(p => new PlayerFullInfoViewModel
                {
                    Age = p.Age,
                    TeamId = p.Team.Id,
                    Height = p.Height,
                    Salary = p.Salary,
                    Position = p.Position,
                    FullName = p.FirstName + " " + p.LastName,
                    PictureURL = p.PictureURL,
                    TeamLogoUrl = p.Team.LogoURL,
                    PointsPerGame = p.PointsPerGame,
                    AssistsPerGame = p.AssistsPerGame,
                    ReboundsPerGame = p.ReboundsPerGame
                })
                .SingleAsync();
        }
    }
}
