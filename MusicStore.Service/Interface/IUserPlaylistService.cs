using MusicStore.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Service.Interface
{
    public interface IUserPlaylistService
    {
        List<UserPlaylist> GetAllUserPlaylists(string userId);
        UserPlaylist GetDetailsForUserPlaylist(Guid id);
        void CreateNewUserPlaylist(UserPlaylist a);
        void UpdateExistingUserPlaylist(UserPlaylist a);
        void DeleteUserPlaylist(Guid id);
    }
}
