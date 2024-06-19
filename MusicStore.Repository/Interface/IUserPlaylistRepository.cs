using MusicStore.Domain.DomainModels;

namespace MusicStore.Repository.Interface
{
    public interface IUserPlaylistRepository
    {
        IEnumerable<UserPlaylist> GetAll();
        UserPlaylist Get(Guid id);
        void Insert(UserPlaylist entity);
        void Update(UserPlaylist entity);
        void Delete(UserPlaylist entity);
        public void SaveChanges();
    }
}
