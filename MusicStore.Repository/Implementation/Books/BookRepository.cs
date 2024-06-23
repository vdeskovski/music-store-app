using Microsoft.EntityFrameworkCore;
using MusicStore.Domain.BookDbIntegration;
using MusicStore.Repository.Interface.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Repository.Implementation.Books
{
    public class BookRepository : IBookRepository
    {

        private readonly BookDbContext _dbContext;
        private DbSet<Book> _entities;

        public BookRepository(BookDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = dbContext.Set<Book>();
        }

        public Book Get(Guid id)
        {
            return _entities
                .Include(e => e.Author)
                .First(e => e.Id == id);
        }

        public IEnumerable<Book> GetAll()
        {
            return _entities
                .Include(e => e.Author)
                .AsEnumerable();
        }
    }
}
