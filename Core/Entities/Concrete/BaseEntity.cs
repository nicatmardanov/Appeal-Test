using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Concrete
{
    public class BaseEntity<T> : IEntity<T>
        where T : notnull
    {
        [Key]
        public T Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
