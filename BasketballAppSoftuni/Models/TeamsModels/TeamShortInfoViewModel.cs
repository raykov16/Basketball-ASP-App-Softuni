using System.ComponentModel.DataAnnotations;

namespace BasketballAppSoftuni.Models.TeamsModels
{
    public class TeamShortInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string LogoURL { get; set; }
    }
}
