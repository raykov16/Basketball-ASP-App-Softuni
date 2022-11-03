using System.ComponentModel.DataAnnotations;

namespace BasketballAppSoftuni.Data.Entities
{
    public class Arena
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Seats { get; set;}
        [Required]
        public string PictureURL { get; set; }
        [Required]
        public string Location { get; set; }


    }
}
