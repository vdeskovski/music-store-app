using MusicStore.Domain.DomainModels;

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
