using Core.DataAccess.Dapper;
using Core.Utilities.Constants;
using Dapper;
using DataAccess.Abstract;
using Entities.Models;
using System.Data;

namespace DataAccess.Concrete.Dapper
{
    public class DapperAdvanceDal : DapperEntityRepositoryBase<Advance, Guid>, IAdvanceDal
    {
        private readonly IDbConnection _dbConnection;

        public DapperAdvanceDal(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Advance> GetByIdAsync(Advance entity)
        {
            string query = $"SELECT ad.Id,ad.DocNumber,ad.Tin,ad.Amount,ad.FilePath,ad.CreatedAt FROM {_tableInfo.TableName} ad WITH(NOLOCK) WHERE ad.Id=@Id";

            return await _dbConnection.QueryFirstOrDefaultAsync<Advance>(query, entity);
        }

        public async Task<Guid> AddAsync(Advance entity)
        {
            string query = $"DECLARE @AddedId TABLE (Id uniqueidentifier);" +
                $"INSERT INTO {_tableInfo.TableName} ([DocNumber],[Tin],[Amount],[FilePath]) " +
                "OUTPUT inserted.Id into @AddedId " +
                "VALUES(@DocNumber,@Tin,@Amount,@FilePath);" +
                "SELECT TOP 1 * FROM @AddedId;";

            return await _dbConnection.ExecuteScalarAsync<Guid>(query, entity);
        }

        public async Task<bool> UpdateAmountAsync(Advance entity)
        {
            string query = $"UPDATE {_tableInfo.TableName} SET [Amount]=[Amount]+@Amount,[ModifiedAt]=GETDATE() WHERE Id=@Id";

            return await _dbConnection.ExecuteAsync(query, entity) > 0;
        }

        public async Task<bool> ExistsAsync(Advance entity)
        {
            string query = $"SELECT CASE WHEN EXISTS(SELECT 1 FROM {_tableInfo.TableName} WITH(NOLOCK) WHERE DocNumber=@DocNumber) THEN 1 ELSE 0 END Res";

            return await _dbConnection.ExecuteScalarAsync<bool>(query, entity);
        }
    }
}
