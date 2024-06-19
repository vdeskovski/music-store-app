using Microsoft.EntityFrameworkCore;
using MusicStore.Domain.DomainModels;
using MusicStore.Repository.Interface;

namespace MusicStore.Repository.Implementation
{
    public class UserPlaylistRepository : IUserPlaylistRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<UserPlaylist> entities;
        string errorMessage = string.Empty;

        public UserPlaylistRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<UserPlaylist>();
        }

        public void Delete(UserPlaylist entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public UserPlaylist Get(Guid id)
        {
            return entities
                .Include("User")
                .Include("TrackInUserPlaylists")
                .Include("TrackInUserPlaylists.Track")
                .Include("TrackInUserPlaylists.Track.Album")
                .Include("TrackInUserPlaylists.Track.Album.Artist")
                .First(up => up.Id == id);
        }

        public IEnumerable<UserPlaylist> GetAll()
        {
            return entities
                .Include("User")
                .Include("TrackInUserPlaylists")
                .Include("TrackInUserPlaylists.Track")
                .Include("TrackInUserPlaylists.Track.Album")
                .Include("TrackInUserPlaylists.Track.Album.Artist")
                .AsEnumerable();
        }

        public void Insert(UserPlaylist entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(UserPlaylist entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
