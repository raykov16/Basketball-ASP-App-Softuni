using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasketballAppSoftuni.Data.Entities
{
    public class Match
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int HomeTeamId { get; set; }
        [ForeignKey(nameof(HomeTeamId))]
        public Team HomeTeam { get; set;}
        [Required]
        public int AwayTeamId { get; set; }
        
        [ForeignKey(nameof(AwayTeamId))]
        public Team AwayTeam { get; set; }
        [Required]
        public int ArenaId { get; set;}
        [ForeignKey(nameof(ArenaId))]
        public Arena Arena { get; set; }
        [Required]
        public DateTime GameDate { get; set; }
        [Required]
        public int TicketsAvailable { get; set;}
        public int? HomeTeamPoints { get; set;}
        public int? AwayTeamPoints { get; set;}
        public List<UserMatch> UsersMatches { get; set; } = new List<UserMatch>();
    }
}
