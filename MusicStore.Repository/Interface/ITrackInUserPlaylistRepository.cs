using MusicStore.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Repository.Interface
{
    public interface ITrackInUserPlaylistRepository
    {
        void Insert(TrackInUserPlaylist entity);
        TrackInUserPlaylist Get(Guid id);
        void Delete(TrackInUserPlaylist entity);
    }
}
