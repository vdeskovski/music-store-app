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
    public class AuthorRepository : IAuthorRepository
    {

        private readonly BookDbContext _dbContext;
        private DbSet<Author> _entities;

        public AuthorRepository(BookDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = dbContext.Set<Author>();
        }

        public Author Get(Guid id)
        {
            return _entities.First(e => e.Id == id);
        }

        public IEnumerable<Author> GetAll()
        {
            return _entities.AsEnumerable();
        }
    }
}
