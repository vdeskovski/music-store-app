using MusicStore.Domain.DomainModels;

namespace MusicStore.Domain.BookDbIntegration
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
        public string Image { get; set; }
    }
}
