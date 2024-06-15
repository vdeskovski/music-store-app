using System.ComponentModel.DataAnnotations;

namespace MusicStoreApp.Models
{
    public class Track : BaseEntity
    {
        [Required]
        public string TrackName { get; set; }
        [Required]
        public int Duration { get; set; }
        public Album? Album { get; set; }
        public Guid AlbumId { get; set; }
        public virtual ICollection<TrackInUserPlaylist>? TrackInUserPlaylists{ get; set; }
    }
}
