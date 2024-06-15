namespace MusicStoreApp.Models
{
    public class Album : BaseEntity
    {
        public string AlbumName { get; set; }
        public int AlbumPrice { get; set; }
        public string AlbumImage { get; set; }
        public string AlbumDescription { get; set; }
        public int AlbumRating { get; set; }
        public Artist? Artist { get; set; }
        public Guid ArtistId { get; set; }
        public virtual ICollection<Track>? AlbumTracks { get; set; }
    }
}
