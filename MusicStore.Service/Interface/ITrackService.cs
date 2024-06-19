using MusicStore.Domain.DomainModels;

namespace MusicStore.Service.Interface
{
    public interface ITrackService
    {
        void AddTrackToUserPlaylist(UserPlaylist playlist, Track track);
        void DeleteTrackFromUserPlaylist(Guid id);
        List<Track> GetAllTracks();
        Track GetDetailsForTrack(Guid id);
        void CreateNewTrack(Track a);
        void UpdateExistingTrack(Track a);
        void DeleteTrack(Guid id);
        public void updateTotalTracks();
    }
}
