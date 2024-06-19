using MusicStore.Domain.DomainModels;

namespace MusicStore.Repository.Interface
{
    public interface ITrackInUserPlaylistRepository
    {
        void Insert(TrackInUserPlaylist entity);
        TrackInUserPlaylist Get(Guid id);
        void Delete(TrackInUserPlaylist entity);
    }
}
