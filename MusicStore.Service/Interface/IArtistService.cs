using MusicStore.Domain.DomainModels;

namespace MusicStore.Service.Interface
{
    public interface IArtistService
    {
        List<Artist> GetAllArtists();
        Artist GetDetailsForArtist(Guid id);
        void CreateNewArtist(Artist a);
        void UpdateExistingArtist(Artist a);
        void DeleteArtist(Guid id);
    }
}
