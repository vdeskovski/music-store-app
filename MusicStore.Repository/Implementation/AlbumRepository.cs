using Microsoft.EntityFrameworkCore;
using MusicStore.Domain.DomainModels;
using MusicStore.Domain.Identity;
using MusicStore.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Repository.Implementation
{
    public class AlbumRepository : IAlbumRepository
    {

        private readonly ApplicationDbContext context;
        private DbSet<Album> entities;
        string errorMessage = string.Empty;

        public AlbumRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Album>();
        }

        public void Delete(Album entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public Album Get(Guid id)
        {
            return entities
                .Include(a => a.Artist)
                .Include(a => a.AlbumTracks)
                .First(a => a.Id == id);
        }

        public IEnumerable<Album> GetAll()
        {
            return entities.
                Include(a => a.Artist)
                .AsEnumerable();
        }

        public void Insert(Album entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(Album entity)
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
