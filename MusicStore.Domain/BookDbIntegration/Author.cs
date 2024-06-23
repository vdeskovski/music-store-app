using MusicStore.Domain.DomainModels;

namespace MusicStore.Domain.BookDbIntegration
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
