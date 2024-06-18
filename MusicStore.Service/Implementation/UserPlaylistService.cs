using MusicStore.Domain.DomainModels;
using MusicStore.Repository.Interface;
using MusicStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Service.Implementation
{
    public class UserPlaylistService : IUserPlaylistService
    {
        private readonly IUserPlaylistRepository _userPlaylistRepository;

        public UserPlaylistService(IUserPlaylistRepository userPlaylistRepository)
        {
            _userPlaylistRepository = userPlaylistRepository;
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

        public List<UserPlaylist> GetAllUserPlaylists()
        {
            return _userPlaylistRepository.GetAll().ToList();
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
