using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BasketballAppSoftuni.Data.Entities
{
    public class UserMatch
    {
        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public MyUser User { get; set; }

        [Required]
        public int MatchId { get; set; }

        [ForeignKey(nameof(MatchId))]
        public Match Match { get; set; }
    }
}
