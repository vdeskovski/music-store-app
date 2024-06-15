using System.ComponentModel.DataAnnotations;

namespace MusicStoreApp.Models
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
