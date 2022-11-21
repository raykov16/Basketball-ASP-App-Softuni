using BasketballAppSoftuni.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BasketballAppSoftuni.Models.MatchViewModels
{
    public class MatchTableViewModel
    {
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public string HomeTeamLogo { get; set;}
        public string HomeTeamName { get; set;}
        public string AwayTeamLogo { get; set;}
        public string AwayTeamName { get; set;}
        public int? HomeTeamPoints { get; set; }
        public int? AwayTeamPoints { get; set; }
    }
}
