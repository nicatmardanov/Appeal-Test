using Core.Entities.Concrete;
using Core.Utilities.Pagination;

namespace Core.DataAccess
{
    public interface IEntityRepository<EntityType, IdType>
        where EntityType : BaseEntity<IdType>, new()
        where IdType : notnull
    {
        Task<EntityType?> GetByIdAsync(EntityType entity);
        Task<IReadOnlyCollection<EntityType?>?> GetAsync(EntityType? entity, PagingOptions? pagingOptions = null);
        Task<IdType> AddAsync(EntityType entity);
        Task<bool> UpdateAsync(EntityType entity);
        Task<bool> DeleteAsync(EntityType entity);
        Task<bool> ExistsAsync(EntityType? entity);
    }
}
