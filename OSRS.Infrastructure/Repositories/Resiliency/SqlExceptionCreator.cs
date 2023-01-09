using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Data.SqlClient;

namespace OSRS.Infrastructure.Repositories.Resiliency
{
    public static class SqlExceptionCreator
    {
        public static SqlException Create(int number, string message="")
        {
            var c = typeof(SqlErrorCollection).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            var errors = (c[0].Invoke(null) as SqlErrorCollection)!;
            var errorList = (errors.GetType().GetField("_errors", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(errors) as List<object>)!;
            c = typeof(SqlError).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            var nineC = c.FirstOrDefault(f => f.GetParameters().Length == 9)!;
            var sqlError = (nineC.Invoke(new object [] { number, (byte)0, (byte)0, "", "", "", (int)0, (uint)0, null }) as SqlError)!;
            errorList.Add(sqlError);
            var ex = (Activator.CreateInstance(typeof(SqlException), BindingFlags.NonPublic | BindingFlags.Instance, null, new object [] {message, errors,
                null, Guid.NewGuid() }, null) as SqlException)!;
            return ex;
        }
    }
   
}
