using MusicStore.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
