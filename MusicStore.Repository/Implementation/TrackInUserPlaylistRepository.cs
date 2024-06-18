using Microsoft.EntityFrameworkCore;
using MusicStore.Domain.DomainModels;
using MusicStore.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Repository.Implementation
{
    public class TrackInUserPlaylistRepository : ITrackInUserPlaylistRepository
    {

        private readonly ApplicationDbContext context;
        private DbSet<TrackInUserPlaylist> entities;
        string errorMessage = string.Empty;

        public TrackInUserPlaylistRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<TrackInUserPlaylist>();
        }

        public void Delete(TrackInUserPlaylist entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public TrackInUserPlaylist Get(Guid id)
        {
            return entities
                .First(t => t.Id == id);
        }

        public void Insert(TrackInUserPlaylist entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }
    }
}
