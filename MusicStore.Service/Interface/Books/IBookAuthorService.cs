using MusicStore.Domain.BookDbIntegration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Service.Interface.Books
{
    public interface IBookAuthorService
    {
        List<Book> getAllBooks();
        Book getBook(Guid id);

        List<Author> getAllAuthors();
        Author getAuthor(Guid id);
    }
}
