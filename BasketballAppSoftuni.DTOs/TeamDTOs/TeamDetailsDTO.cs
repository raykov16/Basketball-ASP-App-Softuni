using BasketballAppSoftuni.Data.Entities;
using BasketballAppSoftuni.DTOs.PlayerDTOs;

namespace BasketballAppSoftuni.DTOs.TeamDTOs
{
    public class TeamDetailsDTO
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Wins { get; set; }
            public int Loses { get; set; }
            public string HomeTown { get; set; }
            public string LogoURL { get; set; }
            public Arena Arena { get; set; }

            public List<PlayerShortInfoDTO> Players { get; set; } = new List<PlayerShortInfoDTO>();
    }
}
