using MusicStore.Domain.DomainModels;
using MusicStore.Repository.Implementation;
using MusicStore.Repository.Interface;
using MusicStore.Service.Interface;


namespace MusicStore.Service.Implementation
{
    public class AlbumService : IAlbumService
    {

        private readonly IAlbumRepository _albumRepository;
        private readonly IUserPlaylistRepository _userPlaylistRepository;

        public AlbumService(IAlbumRepository albumRepository, IUserPlaylistRepository userPlaylistRepository)
        {
            _albumRepository = albumRepository;
            this._userPlaylistRepository = userPlaylistRepository;
        }

        public void CreateNewAlbum(Album a)
        {
            _albumRepository.Insert(a);
        }

        public void DeleteAlbum(Guid id)
        {
            var album = _albumRepository.Get(id);
            _albumRepository.Delete(album);
            var userPlaylists = _userPlaylistRepository.GetAll()
                .ToList();

            foreach (var userPlaylist in userPlaylists)
            {
                userPlaylist.TotalTracks = userPlaylist.TrackInUserPlaylists.Count;
            }

            _userPlaylistRepository.SaveChanges();
        }

        public List<Album> GetAllAlbums()
        {
            return _albumRepository.GetAll().ToList();
        }

        public Album GetDetailsForAlbum(Guid id)
        {
            return _albumRepository.Get(id);
        }

        public void UpdateExistingAlbum(Album a)
        {
            _albumRepository.Update(a);
        }
    }
}
