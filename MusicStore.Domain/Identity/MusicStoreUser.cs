using Microsoft.AspNetCore.Identity;
using MusicStore.Domain.DomainModels;

namespace MusicStore.Domain.Identity
{
    public class MusicStoreUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public virtual ICollection<UserPlaylist>? UserPlaylists { get; set; }
    }
}
