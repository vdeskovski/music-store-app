using MusicStore.Domain.DomainModels;

namespace MusicStore.Repository.Interface
{
    public interface IAlbumRepository
    {
        IEnumerable<Album> GetAll();
        Album Get(Guid id);
        void Insert(Album entity);
        void Update(Album entity);
        void Delete(Album entity);
    }
}
