using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace OSRS.Infrastructure.Dapper
{
    public static class ContextExtensions
    {
        public static async Task<IReadOnlyList<TParent>> DapperParentChildQueryAsync<TParent,
            TChild,
            TParentKey>(
            this DbContext context,
            string sql,
            Func<TParent, TParentKey> parentKeySelector,
            Func<TParent, IList<TChild>> childSelector,
            string splitOn = "Id",
            dynamic param = null,
            IDbTransaction transaction = null,
            bool buffered = true,
            int? commandTimeout = null,
            CommandType? commandType = null, CancellationToken cancellationToken = default)
        {
            var cache = new Dictionary<TParentKey, TParent>();
            var cnn = context.Database.GetDbConnection();
            await cnn.QueryAsync<TParent, TChild, TParent>(
                new CommandDefinition(sql,
                    param as object,
                    transaction,
                    commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None,
                    cancellationToken: cancellationToken),
                (parent, child) =>
                {
                    var key = parentKeySelector(parent);
                    if (!cache.ContainsKey(key))
                        cache.Add(key, parent);
                    var cachedParent = cache[key];
                    var children = childSelector(cachedParent);
                    children.Add(child);
                    return cachedParent;
                }, splitOn);

            return cache.Values.AsList();
        }

        public static async Task<IReadOnlyList<TReturn>> DapperQueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(this DbContext context, CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, string split = "Id", CancellationToken cancellationToken = default)
        {
            var cnn = context.Database.GetDbConnection();
            return (await cnn.QueryAsync(command, map, splitOn: split)).AsList();
        }
        
        public static async Task<IReadOnlyList<TReturn>> DapperQueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(this DbContext context, CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, string split = "Id", CancellationToken cancellationToken = default)
        {
            var cnn = context.Database.GetDbConnection();
            return (await cnn.QueryAsync(command, map, splitOn: split)).AsList();
        }

        public static async Task<IEnumerable<TReturn>> DapperQueryAsync<TFirst, TSecond, TThird, TReturn>(this DbContext context, CommandDefinition command, Func<TFirst, TSecond, TThird, TReturn> map, string split = "Id", CancellationToken cancellationToken = default)
        {
            var cnn = context.Database.GetDbConnection();
            return (await cnn.QueryAsync(command, map, splitOn: split)).AsList();
        }
      
        public static async Task<IReadOnlyList<TReturn>> DapperQueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(this DbContext context, CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, string split = "Id", CancellationToken cancellationToken = default)
        {
            var cnn = context.Database.GetDbConnection();
            return (await cnn.QueryAsync(command, map, splitOn: split)).AsList();
        }

        public static async Task<IReadOnlyList<TReturn>> DapperQueryAsync<TFirst, TSecond, TReturn>(this DbContext context, CommandDefinition command, Func<TFirst, TSecond, TReturn> map, string split = "Id", CancellationToken cancellationToken = default)
        {
            var cnn = context.Database.GetDbConnection();
            return (await cnn.QueryAsync(command, map, splitOn: split)).AsList();
        }

        public static async Task<SqlMapper.GridReader> DapperQueryMultipleAsync(this DbContext context, CommandDefinition command)
        {
            var cnn = context.Database.GetDbConnection();
            return await cnn.QueryMultipleAsync(command);
        }

        public static async Task<IReadOnlyList<TReturn>> DapperQueryAsync<TFirst, TSecond, TThird, TReturn>(this DbContext context, string sql, object param, Func<TFirst, TSecond, TThird, TReturn> map, string split = "Id", CancellationToken cancellationToken = default)
        {
            var cnn = context.Database.GetDbConnection();
            return (await cnn.QueryAsync(new CommandDefinition(sql, param, cancellationToken: cancellationToken), map, splitOn: split)).AsList();
        }

        public static async Task<IReadOnlyList<TReturn>> DapperQueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(this DbContext context, string sql, object param, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, string split = "Id", CancellationToken cancellationToken = default)
        {
            var cnn = context.Database.GetDbConnection();
            return (await cnn.QueryAsync(new CommandDefinition(sql, param, cancellationToken: cancellationToken), map, splitOn: split)).AsList();
        }
       
        public static async Task<IReadOnlyList<T>> DapperQueryAsync<T>(this DbContext context, CommandDefinition command)
        {
            var cnn = context.Database.GetDbConnection();
            return (await cnn.QueryAsync<T>(command)).AsList();
        }

        public static async Task<IReadOnlyList<T>> DapperQueryAsync<T>(this DbContext context, string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            var cnn = context.Database.GetDbConnection();
            return (await cnn.QueryAsync<T>(new CommandDefinition(sql, param, transaction, cancellationToken: cancellationToken))).AsList();
        }

        public static async Task<T> DapperFirstOrDefaultAsync<T>(this DbContext context, string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            var cnn = context.Database.GetDbConnection();
            return await cnn.QueryFirstOrDefaultAsync<T>(new CommandDefinition(sql, param, transaction, cancellationToken: cancellationToken));
        }

        public static async Task<T> DapperQuerySingleAsync<T>(this DbContext context, string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            var cnn = context.Database.GetDbConnection();
            return await cnn.QuerySingleAsync<T>(new CommandDefinition(sql, param, transaction, cancellationToken: cancellationToken));
        }

        public static string ToSql(this CommandDefinition command)
        {
            var query = command.CommandText.Replace("@", string.Empty);
            if (command.Parameters is not Dictionary<string, object> param) return string.Empty;

            var parameters = param.Select(x => new SqlParameter(x.Key.Replace("@", string.Empty), x.Value));
            foreach (var prm in parameters)
            {
                switch (prm.SqlDbType)
                {
                    case SqlDbType.Bit:
                        var boolToInt = (bool) prm.Value ? 1 : 0;
                        query = Regex.Replace(query, @"\b" + prm.ParameterName + @"\b", $"{((bool) prm.Value ? 1 : 0)}");
                        break;
                    case SqlDbType.Int:
                        query = Regex.Replace(query, @"\b" + prm.ParameterName + @"\b", $"{prm.Value}");
                        break;
                    case SqlDbType.VarChar:
                        query = Regex.Replace(query, @"\b" + prm.ParameterName + @"\b", $"'{prm.Value}'");
                        break;
                    default:
                        query = Regex.Replace(query, @"\b" + prm.ParameterName + @"\b", $"'{prm.Value}'");
                        break;
                }
            }

            return query;
        }
    }
}