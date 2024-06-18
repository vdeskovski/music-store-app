using MusicStore.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Service.Interface
{
    public interface ITrackService
    {
        void AddTrackToUserPlaylist(UserPlaylist playlist, Track track);
        void DeleteTrackFromUserPlaylist(Guid id, string userId);
        List<Track> GetAllTracks();
        Track GetDetailsForTrack(Guid id);
        void CreateNewTrack(Track a);
        void UpdateExistingTrack(Track a);
        void DeleteTrack(Guid id);
    }
}
