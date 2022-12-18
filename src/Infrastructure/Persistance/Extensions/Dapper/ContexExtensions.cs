using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace OSRS.Persistance.Extensions.Dapper
{
    public static class ContextExtensions
    {
        public static async Task<IReadOnlyList<TReturn>> DapperQueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(this DbContext context, CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, string split = "Id", CancellationToken cancellationToken = default)
        {
            var cnn = context.Database.GetDbConnection();
            return (await cnn.QueryAsync(command, map, splitOn: split)).AsList();
        }
        public static async Task<IReadOnlyList<TReturn>> DapperQueryAsync<TFirst, TSecond, TReturn>(this DbContext context, CommandDefinition command, Func<TFirst, TSecond, TReturn> map, string split = "Id", CancellationToken cancellationToken = default)
        {
            var cnn = context.Database.GetDbConnection();
            return (await cnn.QueryAsync(command, map, splitOn: split)).AsList();
        }
        
        public static async Task<IReadOnlyList<TReturn>> DapperQueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(this DbContext context, CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, string split = "Id", CancellationToken cancellationToken = default)
        {
            var cnn = context.Database.GetDbConnection();
            return (await cnn.QueryAsync(command, map, splitOn: split)).AsList();
        }
        
        public static async Task<IReadOnlyList<T>> DapperQueryAsync<T>(this DbContext context, CommandDefinition command)
        {
            var cnn = context.Database.GetDbConnection();
            return (await cnn.QueryAsync<T>(command)).AsList();
        }
    }
}