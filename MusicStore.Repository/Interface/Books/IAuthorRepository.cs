using MusicStore.Domain.BookDbIntegration;
using MusicStore.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Repository.Interface.Books
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAll();
        Author Get(Guid id);
    }
}
