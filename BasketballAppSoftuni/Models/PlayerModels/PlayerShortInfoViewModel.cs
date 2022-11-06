using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BasketballAppSoftuni.Models.PlayerModels
{
    public class PlayerShortInfoViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PictureURL { get; set; }

    }
}
