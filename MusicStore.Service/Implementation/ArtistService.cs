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
    public class ArtistService : IArtistService
    {
        private readonly IRepository<Artist> _artistRepository;

        public ArtistService(IRepository<Artist> _artistRepository)
        {
            this._artistRepository = _artistRepository;
        }
        public void CreateNewArtist(Artist a)
        {
            if (!_artistRepository.GetAll().Any(z => z.StageName.Equals(a.StageName)))
            {
                _artistRepository.Insert(a);
            }

        }

        public void DeleteArtist(Guid id)
        {
            Artist artist = _artistRepository.Get(id);
            _artistRepository.Delete(artist);
        }

        public List<Artist> GetAllArtists()
        {
            return _artistRepository.GetAll().ToList();
        }

        public Artist GetDetailsForArtist(Guid id)
        {
            return _artistRepository.Get(id);
        }

        public void UpdateExistingArtist(Artist a)
        {
            _artistRepository.Update(a);
        }
    }
}
