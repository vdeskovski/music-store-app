using MusicStore.Domain.DomainModels;

namespace MusicStore.Repository.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(Guid id);
        T Insert(T entity);
        T Update(T entity);
        T Delete(T entity);
        List<T> InsertMany(List<T> entities);
    }
}
