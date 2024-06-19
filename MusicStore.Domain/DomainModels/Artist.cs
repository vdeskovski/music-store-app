namespace MusicStore.Domain.DomainModels
{
    public class Artist : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StageName { get; set; }
        public int Age { get; set; }
        public virtual ICollection<Album>? ArtistAlbums { get; set; }
    }
}
