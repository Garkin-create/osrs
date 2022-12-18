using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using OSRS.Persistance.Helper;

namespace OSRS.Persistance.Services
{
    public class SystemLogger : ISystemLogger
    {
        private readonly LogType[] _emailTypes = { LogType.Error, LogType.Fatal };
        private const string EmailErrorMsg = "Syntax error, command unrecognized. The server response was: 4.3.2 Concurrent connections limit exceeded.";
        
        // private readonly IElasticRepository<LogDio> _logRepository;
        // private readonly IBackgroundService _backgroundService;
        // private readonly LogServiceOptions _options;
        // private readonly IRequestMetadataProvider _requestMetadata;
        // public SystemLogger(IElasticRepository<LogDio> logRepository, IBackgroundService backgroundService, LogServiceOptions options, IRequestMetadataProvider requestMetadata = null)
        // {
        //     _logRepository = logRepository;
        //     _backgroundService = backgroundService;
        //     _options = options;
        //     _requestMetadata = requestMetadata;
        // }

        public SystemLogger()
        {
            
        }
        public void Log(object sender, LogType type, string message = "", Dictionary<LogDetailsType, object> additionalData = null, [CallerMemberName] string methodName = "")
        {
            try
            {   return;
                // var username = _requestMetadata?.Username;
                // var userAgent = _requestMetadata?.GetCurrentUserAgent();
                // var currentIp = _requestMetadata?.GetIpAddress();
                Dictionary<LogDetailsType, string> details = new();
                if (sender != null)
                    details.Add(LogDetailsType.Class, sender.GetType().Name);

                details.Add(LogDetailsType.Method, methodName);

                additionalData ??= new();
                foreach (var i in additionalData.Keys)
                {
                    if (additionalData[i] is string textValue)
                        details.Add(i, textValue);
                    else
                    {
                        string value;
                        try
                        {
                            value = JsonSerializer.Serialize(additionalData[i]);   
                        }
                        catch (Exception exc)
                        {
                            value = $"Unable to serialize: {exc.Message}";
                        }
                        details.Add(i, value);
                    }
                }

                // #region "Extra Data"
                // if(!additionalData.Keys.Contains(LogDetailsType.Username) && username != null)
                //     details.Add(LogDetailsType.Username, username);
                // if(!additionalData.Keys.Contains(LogDetailsType.RequestIpAddress) && currentIp != null)
                //     details.Add(LogDetailsType.RequestIpAddress, currentIp);
                // if(!additionalData.Keys.Contains(LogDetailsType.RequestUserAgent) && userAgent != null)
                //     details.Add(LogDetailsType.RequestUserAgent, userAgent.Value.ToString());
                // #endregion
                //
                // _backgroundService.EnqueueAction(() =>
                //     PersistLog(new ()
                //     {
                //         Type = type,
                //         Message = message,
                //         CreatedDate = DateTime.Now,
                //         Details = details,
                //     })
                // );
            }
            catch (Exception e)
            {
                Console.WriteLine($@"Error Logging::{methodName} -- {e.Message}");
            }
        }

        public Task LogAsync(object sender, LogType type, string message = "", Dictionary<LogDetailsType, object> additionalData = null, CancellationToken cancellationToken = default, [CallerMemberName] string methodName = "")
        {
            return Task.Run(() => Log(sender, type, message, additionalData, methodName), cancellationToken);
        }

        public void LogException(object sender, Exception e, string message = "", Dictionary<LogDetailsType, object> additionalData = null, [CallerMemberName] string methodName = "")
        {
            var t = LogExceptionAsync(sender, e, message, additionalData, default, methodName);
            t.Wait();
        }

        public async Task LogExceptionAsync(object sender, Exception e, string message = "", Dictionary<LogDetailsType, object> additionalData = null, CancellationToken cancellationToken = default, [CallerMemberName] string methodName = "")
        {
            additionalData ??= new();
            additionalData.Add(LogDetailsType.ExceptionMessage, e.Message);
            if (string.IsNullOrEmpty(message))
                message = e.Message;
            await LogAsync(sender, LogType.Error, message, additionalData, cancellationToken, methodName);
        }

        // ReSharper disable once MemberCanBePrivate.Global
        // public void PersistLog(LogDio log)
        // {
        //     if (_emailTypes.Contains(log.Type))
        //         SendEmailLog(log);
        //     _logRepository.Add(log);
        // }

        // private void SendEmailLog(LogDio log)
        // {
        //     if (string.IsNullOrEmpty(_options.EmailServer.Server)) return;
        //     try
        //     {
        //         var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        //         using var smtp = new SmtpClient(_options.EmailServer.Server, _options.EmailServer.Port);
        //         smtp.Credentials = new NetworkCredential(_options.EmailServer.Username, _options.EmailServer.Password);
        //         smtp.EnableSsl = _options.EmailServer.UseSSL;
        //         foreach (var recipient in _options.EmailList)
        //         {
        //             var messageBody = new MailMessage();
        //             messageBody.To.Add(new MailAddress(recipient));
        //             messageBody.Subject = $"{log.Type} Notification ({env})";
        //             messageBody.From = new(_options.EmailServer.Username);
        //             messageBody.Body = log.ToHtmlString();
        //             messageBody.IsBodyHtml = true;
        //             smtp.Send(messageBody);
        //         }
        //     }
        //     catch(SmtpException exc)
        //     {
        //         if (!exc.Message.Contains(EmailErrorMsg))
        //             LogException(this, exc);
        //     }
        //     catch(Exception exc)
        //     {
        //         LogException(this, exc);
        //         throw;
        //     }
        // }
    }
}
