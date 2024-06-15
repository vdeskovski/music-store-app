using Microsoft.AspNetCore.Identity;

namespace MusicStoreApp.Models
{
    public class MusicStoreUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public virtual ICollection<UserPlaylist>? UserPlaylists { get; set; }
    }
}
