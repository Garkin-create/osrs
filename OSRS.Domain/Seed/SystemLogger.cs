using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OSRS.Domain.Seed
{
    public class SystemLogger: ISystemLogger
    {
        public Task LogAsync(object sender, LogType type, string message = "", Dictionary<LogDetailsType, object> additionalData = null,
            CancellationToken cancellationToken = default, string methodName = "")
        {
            throw new NotImplementedException();
        }

        public void Log(object sender, LogType type, string message = "", Dictionary<LogDetailsType, object> additionalData = null, string methodName = "")
        {
            throw new NotImplementedException();
        }

        public Task LogExceptionAsync(object sender, Exception e, string message = "", Dictionary<LogDetailsType, object> additionalData = null,
            CancellationToken cancellationToken = default, string methodName = "")
        {
            throw new NotImplementedException();
        }

        public void LogException(object sender, Exception e, string message = "", Dictionary<LogDetailsType, object> additionalData = null,
            string methodName = "")
        {
            throw new NotImplementedException();
        }
    }
}