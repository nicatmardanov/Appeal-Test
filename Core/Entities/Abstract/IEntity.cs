namespace Core.Entities
{
    public interface IEntity<T>
        where T : notnull
    {
        public T Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime ModifiedAt { get; set; }
    }
}
