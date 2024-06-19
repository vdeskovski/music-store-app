using MusicStore.Domain.DomainModels;

namespace MusicStore.Repository.Interface
{
    public interface ITrackRepository
    {
        IEnumerable<Track> GetAll();
        Track Get(Guid id);
        void Insert(Track entity);
        void Update(Track entity);
        void Delete(Track entity);
    }
}
