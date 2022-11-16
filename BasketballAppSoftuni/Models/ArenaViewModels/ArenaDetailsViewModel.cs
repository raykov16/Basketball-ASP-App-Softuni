using System.ComponentModel.DataAnnotations;

namespace BasketballAppSoftuni.Models.ArenaViewModels
{
    public class ArenaDetailsViewModel
    {
        public string Name { get; set; }
        public int Seats { get; set; }
        public string PictureURL { get; set; }
        public string Location { get; set; }
    }
}
