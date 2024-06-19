using MusicStore.Domain.DomainModels;
using MusicStore.Repository.Interface;
using MusicStore.Service.Interface;

namespace MusicStore.Service.Implementation
{
    public class UserPlaylistService : IUserPlaylistService
    {
        private readonly IUserPlaylistRepository _userPlaylistRepository;
        private readonly IUserRepository _userRepository;

        public UserPlaylistService(
            IUserPlaylistRepository userPlaylistRepository, 
            IUserRepository userRepository)
        {
            _userPlaylistRepository = userPlaylistRepository;
            _userRepository = userRepository;
        }

        public void CreateNewUserPlaylist(UserPlaylist a)
        {
            _userPlaylistRepository.Insert(a);
        }

        public void DeleteUserPlaylist(Guid id)
        {
            var userPlaylist = _userPlaylistRepository.Get(id);
            _userPlaylistRepository.Delete(userPlaylist);
        }

        public List<UserPlaylist> GetAllUserPlaylists(string userId)
        {
            var user = _userRepository.Get(userId);
            return _userPlaylistRepository.GetAll().Where(z => z.User == user).ToList();
        }

        public UserPlaylist GetDetailsForUserPlaylist(Guid id)
        {
            return _userPlaylistRepository.Get(id);
        }

        public void UpdateExistingUserPlaylist(UserPlaylist a)
        {
            _userPlaylistRepository.Update(a);
        }
    }
}
