using System.ComponentModel.DataAnnotations;

namespace BasketballAppSoftuni.DTOs.ManagerAreaDTOs
{
    public class ScheduleMatchDTO
    {
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        [Required]
        public DateTime MatchDate { get; set; }
        [Required(ErrorMessage = "Match time is requierd!")]
        public string MatchTime { get; set; }
        [Required]
        public decimal TicketPrice { get; set; }

        public IEnumerable<TeamShortInfoDTO>? AllTeams { get; set; } 
    }
}
