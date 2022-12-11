using System.ComponentModel.DataAnnotations;

namespace BasketballAppSoftuni.Areas.Manager.Models
{
    public class UpdateMatchResultModel
    {
        public int MatchId { get; set; }
        public string MatchDate { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        [Required]
        [Range(0,250)]
        public int HomeTeamPoints { get; set;}
        [Required]
        [Range(0, 250)]
        public int AwayTeamPoints { get; set;}
    }
}