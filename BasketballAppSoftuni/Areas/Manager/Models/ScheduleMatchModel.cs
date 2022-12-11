using BasketballAppSoftuni.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BasketballAppSoftuni.Areas.Manager.Models
{
    public class ScheduleMatchModel
    {
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        [Required]
        public DateTime MatchDate { get; set; }
        [Required(ErrorMessage = "Match time is requierd!")]
        public string MatchTime { get; set; }
        [Required]
        [Range(typeof(decimal),"0.00","120.00")]
        public decimal TicketPrice { get; set; }

        public IEnumerable<TeamShortInfoModel>? AllTeams { get; set; } 
    }
}
