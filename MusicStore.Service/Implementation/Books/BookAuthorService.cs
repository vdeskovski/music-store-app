using MusicStore.Domain.BookDbIntegration;
using MusicStore.Repository.Interface.Books;
using MusicStore.Service.Interface.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Service.Implementation.Books
{
    public class BookAuthorService : IBookAuthorService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookAuthorService(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public List<Author> getAllAuthors()
        {
            return _authorRepository.GetAll().ToList();
        }

        public List<Book> getAllBooks()
        {
            return _bookRepository.GetAll().Where(b => b.Description.Contains("comedy")).ToList();
        }

        public Author getAuthor(Guid id)
        {
            return _authorRepository.Get(id);
        }

        public Book getBook(Guid id)
        {
            return _bookRepository.Get(id);
        }
    }
}
