using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Concrete
{
    public class BaseEntity<T> : IEntity<T>
        where T : notnull
    {
        [Key]
        public T Id { get; set; }
        public long CreatedAt { get; set; }
        public long ModifiedAt { get; set; }
    }
}
