using Microsoft.EntityFrameworkCore;
using MusicStore.Domain.Identity;
using MusicStore.Repository.Interface;

namespace MusicStore.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<MusicStoreUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<MusicStoreUser>();
        }
        public void Delete(MusicStoreUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(user);
            context.SaveChanges();
        }

        public MusicStoreUser Get(string id)
        {
            var strGuid = id.ToString();
            return entities.Include("UserPlaylists")
                .Include("UserPlaylists.TrackInUserPlaylists")
                .Include("UserPlaylists.TrackInUserPlaylists.Track")
                .Include("UserPlaylists.TrackInUserPlaylists.Track.Album")
                .Include("UserPlaylists.TrackInUserPlaylists.Track.Album.Artist")
                .First(z => z.Id == strGuid);
        }

        public IEnumerable<MusicStoreUser> GetAll()
        {
            return entities
                .AsEnumerable();
        }

        public void Insert(MusicStoreUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(user);
            context.SaveChanges();
        }

        public void Update(MusicStoreUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(user);
            context.SaveChanges();
        }
    }
}
