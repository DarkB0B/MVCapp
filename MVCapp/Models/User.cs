using Microsoft.AspNetCore.Identity;

namespace MVCapp.Models
{
    public class User : IdentityUser
    {
        public List<Team> FavouriteTeams { get; set; } = new List<Team>();
    }
}

