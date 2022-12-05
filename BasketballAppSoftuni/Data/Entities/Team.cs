using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasketballAppSoftuni.Data.Entities
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set;}

        public int Wins { get; set;}

        public int Loses { get; set; }
        [Required]
        public string HomeTown { get; set; }
        [Required]
        public string LogoURL { get; set; }
        [Required]
        public int ArenaId { get; set;}
        [ForeignKey(nameof(ArenaId))]
        public Arena Arena { get; set; }

        public List<Player> Players { get; set;  } = new List<Player>();
    }
}
