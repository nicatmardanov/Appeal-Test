namespace Core.Utilities.Mapper
{
    public interface IMapper
    {
        Destination Map<Source, Destination>(Source source, bool reverseMap = false);
    }
}
