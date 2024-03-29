﻿using BasketballAppSoftuni.Data.Entities;
using BasketballAppSoftuni.Models.PlayerModels;

namespace BasketballAppSoftuni.Models.TeamsModels
{
    public class TeamDetailsViewModel
    {

            public int Id { get; set; }
            public string Name { get; set; }
            public int Wins { get; set; }
            public int Loses { get; set; }
            public string HomeTown { get; set; }
            public string LogoURL { get; set; }
            public Arena Arena { get; set; }
            public IEnumerable<PlayerShortInfoViewModel> Players { get; set; } = new List<PlayerShortInfoViewModel>();

    }
}
