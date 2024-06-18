using MusicStore.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Repository.Interface
{
    public interface IUserPlaylistRepository
    {
        IEnumerable<UserPlaylist> GetAll();
        UserPlaylist Get(Guid id);
        void Insert(UserPlaylist entity);
        void Update(UserPlaylist entity);
        void Delete(UserPlaylist entity);
    }
}
