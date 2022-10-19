using Auth.Core.Utilities.Pagination;
using Core.DataAccess.Dapper;
using Core.Utilities.Constants;
using Core.Utilities.Requests;
using Dapper;
using DataAccess.Abstract;
using Entities.Dtos.Transfer;
using Entities.Models;
using System.Data;

namespace DataAccess.Concrete.Dapper
{
    public class DapperTransferDal : DapperEntityRepositoryBase<Transfer, int>, ITransferDal
    {
        private readonly IDbConnection _dbConnection;

        public DapperTransferDal(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Transfer?> GetByIdAsync(Transfer entity)
        {
            string query = "SELECT tr.Id,tr.Amount,tr.AdvanceId,tr.CreatedAt,ad.Id,ad.DocNumber,ad.Tin,ad.Amount,ad.FilePath,ad.CreatedAt " +
                          $"FROM {_tableInfo.TableName} tr WITH(NOLOCK) " +
                           "INNER JOIN [Payment].[Advance] ad WITH (NOLOCK) ON tr.AdvanceId = ad.Id " +
                           "WHERE tr.Id=@Id";

            return (await _dbConnection.QueryAsync<Transfer, Advance, Transfer?>(query, (transfer, advance) =>
            {
                if (transfer is not null && advance is not null)
                    transfer.Advance = advance;

                return transfer;
            }, param: entity)).FirstOrDefault();
        }

        public async Task<string?> GetFilePathAsync(Transfer entity)
        {
            string query = $"SELECT ad.FilePath FROM {_tableInfo.TableName} tr WITH(NOLOCK) INNER JOIN [Payment].[Advance] ad WITH (NOLOCK) ON tr.AdvanceId = ad.Id WHERE tr.Id=@Id";
            return await _dbConnection.ExecuteScalarAsync<string?>(query, entity);
        }

        public async Task<List<Transfer>> GetAsync(DataRequest<TransferGetListRequestDto> requestDto)
        {
            string query = "SELECT [pt].[Id],[pt].[Amount],[pt].[CreatedAt],[pt].[AdvanceId],[pa].[Id],[pa].[DocNumber],[pa].[Tin],[pa].[Amount],[pa].[CreatedAt] " +
                           "FROM [Payment].[Transfer] pt WITH(NOLOCK) INNER JOIN [Payment].[Advance] pa WITH(NOLOCK) ON pt.AdvanceId=pa.Id " +
                           GetFilterCondition(requestDto.Parameters is null) +
                           _tableInfo.OrderBy +
                           PaginationHelper.LimitQuery(requestDto.PagingOptions);


            return (await _dbConnection.QueryAsync<Transfer, Advance, Transfer>(query, (transfer, advance) =>
            {
                if (transfer is not null && advance is not null)
                    transfer.Advance = advance;

                return transfer!;
            }, param: requestDto.Parameters)).ToList();
        }

        public async Task<int> AddAsync(Transfer entity)
        {
            string query = $"INSERT INTO {_tableInfo.TableName} ([Amount],[AdvanceId]) VALUES(@Amount,@AdvanceId);" + DbConstants.ScopeIdentity;

            return await _dbConnection.ExecuteScalarAsync<int>(query, entity);
        }

        public async Task<bool> UpdateAsync(Transfer entity)
        {
            string query = $"UPDATE {_tableInfo.TableName} SET [Amount]=@Amount,[ModifiedAt]=GETDATE() WHERE Id=@Id";

            return await _dbConnection.ExecuteAsync(query, entity) > 0;
        }

        public async Task<bool> DeleteAsync(Transfer entity)
        {
            string query = $"DELETE FROM {_tableInfo.TableName} WHERE Id=@Id";

            return await _dbConnection.ExecuteAsync(query, entity) > 0;
        }

        private string? GetFilterCondition(bool entityIsNull)
        {
            return entityIsNull
                ? null
                : "WHERE (pa.Id = @AdvanceId OR @AdvanceId=CAST(0x0 as uniqueidentifier)) " +
                  "AND (pa.DocNumber = @DocNumber or @DocNumber IS NULL) " +
                  "AND (pa.Tin = @Tin or @Tin IS NULL) ";
        }
    }
}
