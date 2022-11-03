using Microsoft.AspNetCore.Identity;

namespace BasketballAppSoftuni.Data.Entities
{
    public class MyUser : IdentityUser
    {
        public List<Match> MyMatches { get; set; } = new List<Match>();
    }
}
