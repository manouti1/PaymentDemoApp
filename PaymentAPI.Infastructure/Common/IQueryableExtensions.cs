using Dapper;
using System.Data;

namespace PaymentAPI.Infastructure.Common
{
    public static class DapperExtensions
    {
        public static async Task<Domain.Common.PaginatedList<T>> GetPagedAsync<T>(this IDbConnection connection, string sql, object parameters, int pageIndex, int pageSize)
        {
            var items = await connection.QueryAsync<T>(sql,
                                                       parameters);

            var countSql = $"SELECT COUNT(1) FROM ({sql}) AS CountQuery";
            var totalItems = await connection.ExecuteScalarAsync<int>(countSql,
                                                                      parameters);

            return new Domain.Common.PaginatedList<T>(items.ToList(), totalItems, pageIndex, pageSize);
        }
    }
}
