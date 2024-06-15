using MusicStore.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Domain.DomainModels
{
    public class UserPlaylist : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? TotalTracks { get; set; } = 0;

        public MusicStoreUser? User { get; set; }
        public virtual ICollection<TrackInUserPlaylist>? TrackInUserPlaylists { get; set; }
    }
}
