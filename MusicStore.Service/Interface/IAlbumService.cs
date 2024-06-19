using MusicStore.Domain.DomainModels;

namespace MusicStore.Service.Interface
{
    public interface IAlbumService
    {
        List<Album> GetAllAlbums();
        Album GetDetailsForAlbum(Guid id);
        void CreateNewAlbum(Album a);
        void UpdateExistingAlbum(Album a);
        void DeleteAlbum(Guid id);
    }
}
