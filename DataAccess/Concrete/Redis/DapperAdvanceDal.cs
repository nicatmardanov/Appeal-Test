using Core.Utilities.Constants;
using Dapper;
using DataAccess.Abstract;
using Entities.Models;
using System.Data;

namespace DataAccess.Concrete.Redis
{
    public class DapperAdvanceDal : IAdvanceDal
    {
        private readonly IDbConnection _dbConnection;

        public DapperAdvanceDal(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<Guid> AddAsync(Advance entity)
        {
            string query = "INSERT INTO [Payment].[Advance] ([DocNumber],[Tin],[Amount]) VALUES(@DocNumber,@Tin,@Amount);" + DbConstants.ScopeIdentity;

            return await _dbConnection.ExecuteScalarAsync<Guid>(query);
        }
    }
}
