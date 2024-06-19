using System.ComponentModel.DataAnnotations;

namespace MusicStore.Domain.DomainModels
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
