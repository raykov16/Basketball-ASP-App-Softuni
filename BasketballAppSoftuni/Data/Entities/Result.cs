using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasketballAppSoftuni.Data.Entities
{
    public class Result
    {
        [Key]
        public int Id { get; set; }
        public int HomeTeamPoints { get; set; }
        public int AwayTeamPoints { get; set;}
        public int MatchId { get; set;}
        [ForeignKey(nameof(MatchId))]
        public Match Match { get; set; }
    }
}
