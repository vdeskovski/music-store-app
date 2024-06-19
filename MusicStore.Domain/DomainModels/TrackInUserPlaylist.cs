namespace MusicStore.Domain.DomainModels
{
    public class TrackInUserPlaylist : BaseEntity
    {
        public Guid TrackId { get; set; }
        public Guid UserPlaylistId { get; set; }
        public Track? Track { get; set; }
        public UserPlaylist? UserPlaylist { get; set; }
    }
}
