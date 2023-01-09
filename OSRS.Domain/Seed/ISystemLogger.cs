using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace OSRS.Domain.Seed
{
    public interface ISystemLogger
    {
        public Task LogAsync(object sender, LogType type, string message = "", Dictionary<LogDetailsType, object> additionalData = null, CancellationToken cancellationToken = default, [CallerMemberName] string methodName = "");
        public void Log(object sender, LogType type, string message = "", Dictionary<LogDetailsType, object> additionalData = null, [CallerMemberName] string methodName = "");
        public Task LogExceptionAsync(object sender, Exception e, string message = "", Dictionary<LogDetailsType, object> additionalData = null, CancellationToken cancellationToken = default, [CallerMemberName] string methodName = "");
        public void LogException(object sender, Exception e, string message = "", Dictionary<LogDetailsType, object> additionalData = null, [CallerMemberName] string methodName = "");

    }

    public enum LogType
    {
        Debug, Alert, Error, Fatal
    }

    public enum LogDetailsType
    {
        Class, Method, ExceptionMessage, Username, RequestData, RequestIpAddress, RequestUserAgent
    }
}
