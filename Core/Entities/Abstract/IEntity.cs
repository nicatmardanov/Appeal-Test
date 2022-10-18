namespace Core.Entities
{
    public interface IEntity<T>
        where T : notnull
    {
        public T Id { get; set; }
        long CreatedAt { get; set; }
        long ModifiedAt { get; set; }
    }
}
