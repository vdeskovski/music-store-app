namespace MusicStoreApp.Models
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
