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
    public class TrackRepository : ITrackRepository
    {

        private readonly ApplicationDbContext context;
        private DbSet<Track> entities;
        string errorMessage = string.Empty;

        public TrackRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Track>();
        }

        public void Delete(Track entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public Track Get(Guid id)
        {
            return entities
                .Include(t => t.Album)
                .First(t => t.Id == id);
        }

        public IEnumerable<Track> GetAll()
        {
            return entities
                .Include(t => t.Album)
                .AsEnumerable();
        }

        public void Insert(Track entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(Track entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }
    }
}
