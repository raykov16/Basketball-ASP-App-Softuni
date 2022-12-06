using Microsoft.AspNetCore.Identity;

namespace BasketballAppSoftuni.Data.Entities
{
    public class MyUser : IdentityUser
    {
        public List<UserMatch> UserMatches { get; set; } = new List<UserMatch>();
    }
}
