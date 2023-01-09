using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace OSRS.Infrastructure.Helper
{
    public static class SqlQueryExtensions
    {
        public static IList<T> SqlQuery<T>(this DbContext db, string sql, Dictionary<string, object> parameters) where T : class
        {
            var t = Task.Run(async () => await SqlQueryAsync<T>(db, sql, parameters));
            return t.Result;
        }
        public static async Task<IList<T>> SqlQueryAsync<T>(this DbContext db, string sql, Dictionary<string, object> parameters, CancellationToken cancellationToken = default) where T : class
        {
            using var db2 = new ContextForQuery<T>(db.Database.GetDbConnection());
            IQueryable<T> query;
            if (parameters != null && parameters.Any())
                query = db2.Data.FromSqlRaw(sql, GetParameters(parameters));
            else
                query = db2.Data.FromSqlRaw(sql);
            return await query.ToListAsync(cancellationToken);
        }

        public static async Task<IList<T>> SqlKataQueryAsync<T>(this DbContext db, string sql, Dictionary<string, object> parameters, CancellationToken cancellationToken = default) where T : class
        {  if (string.IsNullOrEmpty(sql)) return null;
            using var db2 = new ContextForQuery<T>(db.Database.GetDbConnection());
            IQueryable<T> query;
            if (parameters != null && parameters.Any())
                query = db2.Data.FromSqlRaw(sql, GetParameters(parameters,string.Empty));
            else
                query = db2.Data.FromSqlRaw(sql);
            return await query.ToListAsync(cancellationToken);
        }
        public static IList<T> SqlProcedure<T>(this DbContext db, string sql, Dictionary<string, object> parameters) where T : class
        {
            var t = Task.Run(async () => await SqlProcedureAsync<T>(db, sql, parameters));
            return t.Result;
        }

        public static Task<IList<T>> SqlProcedureAsync<T>(this DbContext db, string sql, Dictionary<string, object> parameters, CancellationToken cancellationToken = default) where T : class
        {
            if(parameters != null && parameters.Any())
                return SqlQueryAsync<T>(db, $"exec {sql} {string.Join(',', parameters.Select(p => $"@{p.Key}"))}", parameters, cancellationToken);
            return SqlQueryAsync<T>(db, $"exec {sql}", parameters, cancellationToken);
        }

        public static CountProcedureResult<T> SqlCountProcedure<T>(this DbContext db, string sql, Dictionary<string, object> parameters) where T : class
        {
            var t = Task.Run(async () => await SqlCountProcedureAsync<T>(db, sql, parameters));
            return t.Result;
        }
        public static async Task<CountProcedureResult<T>> SqlCountProcedureAsync<T>(this DbContext db, string sql, Dictionary<string, object> parameters, CancellationToken cancellationToken = default) where T : class
        {
            using var db2 = new ContextForQuery<T>(db.Database.GetDbConnection(), true);
            IQueryable<T> query;
            IList<T> result;
            T temp;
            int total = 0;
            if (parameters != null && parameters.Any())
                query = db2.Data.FromSqlRaw($"exec {sql} {string.Join(',', parameters.Select(p => $"@{p.Key} = @{p.Key}"))}", GetParameters(parameters));
            else
                query = db2.Data.FromSqlRaw($"exec {sql}");

            result = await query.ToListAsync(cancellationToken);
            if((temp = result.FirstOrDefault()) != null)
                total = db2.Entry(temp).Property<int>("TotalCount").CurrentValue;
            return new CountProcedureResult<T>()
            {
                Data = result,
                Count = total
            };
        }
        private static SqlParameter[] GetParameters(Dictionary<string, object> parameters, string prefix="@")
        {
            var sqlParameters = new List<SqlParameter>();
            foreach (var item in parameters)
            {
                if (item.Value != null && item.Value.GetType().IsArray && item.Value.GetType() == typeof(int[]))
                {
                    var dt = new DataTable { Columns = { new DataColumn("Item", typeof(int)) } };
                    foreach (var id in (int[])item.Value) dt.Rows.Add(id);

                    var parameter = new SqlParameter($"{prefix}{item.Key}", dt) { SqlDbType = SqlDbType.Structured, TypeName = "dbo.IntList" };
                    sqlParameters.Add(parameter);
                }
                else if (item.Value != null && item.Value.GetType() == typeof(List<string>))
                {
                    var dt = new DataTable { Columns = { new DataColumn("Item", typeof(string)) } };
                    foreach (var value in (List<string>)item.Value) dt.Rows.Add(value);

                    var parameter = new SqlParameter($"{prefix}{item.Key}", dt) { SqlDbType = SqlDbType.Structured, TypeName = "dbo.StringList" };
                    sqlParameters.Add(parameter);
                }
                else
                    sqlParameters.Add(new SqlParameter($"{prefix}{item.Key}", item.Value ?? DBNull.Value));
            }
            return sqlParameters.ToArray();
        }

        public static IEnumerable<T> Except<T, TKey>(this IEnumerable<T> items, IEnumerable<T> other, Func<T, TKey> getKeyFunc)
        {
            return items
                .GroupJoin(other, getKeyFunc, getKeyFunc, (item, tempItems) => new { item, tempItems })
                .SelectMany(t => t.tempItems.DefaultIfEmpty(), (t, temp) => new { t, temp })
                .Where(t => t.temp is null || t.temp.Equals(default(T)))
                .Select(t => t.t.item);
        }
        private class ContextForQuery<T> : DbContext where T : class
        {
            private readonly DbConnection connection;
            private bool _hasTracking;
            internal DbSet<T> Data { get; set; }
            public ContextForQuery(DbConnection connection, bool track = false){ 
                this.connection = connection;
                if (!(_hasTracking = track))
                    ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                Data = Set<T>();
            }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
                optionsBuilder.ConfigureWarnings(p => p.Ignore(SqlServerEventId.ConflictingValueGenerationStrategiesWarning));
                optionsBuilder.UseSqlServer(connection, options => options.EnableRetryOnFailure());
                base.OnConfiguring(optionsBuilder);
            }
            protected override void OnModelCreating(ModelBuilder modelBuilder){
                if(!_hasTracking)
                    modelBuilder.Entity<T>().HasNoKey();
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
                base.OnModelCreating(modelBuilder);
            }
        }
    }

    public class CountProcedureResult<T>
    {
        public IList<T> Data { get; set; }
        public int Count { get; set; }
    }
}
