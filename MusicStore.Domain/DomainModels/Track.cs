using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Domain.DomainModels
{
    public class Track : BaseEntity
    {
        [Required]
        public string TrackName { get; set; }
        [Required]
        public int Duration { get; set; }
        public Album? Album { get; set; }
        public Guid AlbumId { get; set; }
        public virtual ICollection<TrackInUserPlaylist>? TrackInUserPlaylists { get; set; }
    }
}
