using Microsoft.EntityFrameworkCore;
using MusicStore.Domain.DomainModels;
using MusicStore.Repository.Implementation;
using MusicStore.Repository.Interface;
using MusicStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Service.Implementation
{
    public class TrackService : ITrackService
    {

        private readonly ITrackRepository _trackRepository;
        private readonly ITrackInUserPlaylistRepository _trackInUserPlaylistRepository;
        private readonly IUserRepository _userRepository;

        public TrackService(ITrackRepository trackRepository, 
            ITrackInUserPlaylistRepository trackInUserPlaylistRepository,
            IUserRepository userRepository)
        {
            _trackRepository = trackRepository;
            _trackInUserPlaylistRepository = trackInUserPlaylistRepository;
            _userRepository = userRepository;
        }

        public void AddTrackToUserPlaylist(UserPlaylist selectedPlaylist, Track selectedTrack)
        {
            if (selectedPlaylist != null && selectedTrack != null)
            {
                if (selectedPlaylist.TrackInUserPlaylists == null)
                {
                    selectedPlaylist.TrackInUserPlaylists = new List<TrackInUserPlaylist>();
                }
                if (!selectedPlaylist.TrackInUserPlaylists.Any(t => t.TrackId == selectedTrack.Id))
                {
                    var trackInUserPlaylist = new TrackInUserPlaylist
                    {
                        TrackId = selectedTrack.Id,
                        UserPlaylistId = selectedPlaylist.Id,
                        Track = selectedTrack,
                        UserPlaylist = selectedPlaylist,
                        Id = Guid.NewGuid()
                    };
                    selectedPlaylist.TotalTracks += 1;
                    _trackInUserPlaylistRepository.Insert(trackInUserPlaylist);
                }
            }
        }

        public void DeleteTrackFromUserPlaylist(Guid id, string userId)
        {
            var createdBy = _userRepository.Get(userId);
            var playlists = createdBy.UserPlaylists;
            var selectedTrackInPlaylist = _trackInUserPlaylistRepository.Get(id);
            var selectedPlaylist = selectedTrackInPlaylist.UserPlaylist;
            if (selectedTrackInPlaylist != null)
            {
                _trackInUserPlaylistRepository.Delete(selectedTrackInPlaylist);
                selectedPlaylist.TotalTracks -= 1;
            }
        }

        public void CreateNewTrack(Track a)
        {
            _trackRepository.Insert(a);
        }

        public void DeleteTrack(Guid id)
        {
            var track = _trackRepository.Get(id);
            _trackRepository.Delete(track);
        }

        public List<Track> GetAllTracks()
        {
            return _trackRepository.GetAll().ToList();
        }

        public Track GetDetailsForTrack(Guid id)
        {
            return _trackRepository.Get(id);
        }

        public void UpdateExistingTrack(Track a)
        {
            _trackRepository.Update(a);
        }
    }
}
