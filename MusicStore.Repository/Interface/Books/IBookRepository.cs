using MusicStore.Domain.BookDbIntegration;
using MusicStore.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Repository.Interface.Books
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Book Get(Guid id);
    }
}
