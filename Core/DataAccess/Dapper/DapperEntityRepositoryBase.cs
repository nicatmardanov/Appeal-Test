using Core.Entities.Concrete;
using Core.Utilities.Attributes;
using System.Reflection;

namespace Core.DataAccess.Dapper
{
    public class DapperEntityRepositoryBase<EntityType, IdType> : IEntityRepository
        where EntityType : BaseEntity<IdType>, new()
        where IdType : notnull
    {
        protected readonly TableInfoAttribute _tableInfo;
        public DapperEntityRepositoryBase()
        {
            _tableInfo = typeof(EntityType).GetCustomAttribute<TableInfoAttribute>(true)!;
        }
    }
}
