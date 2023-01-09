using System.ComponentModel;
using OSRS.Application.Seed.Models.Output;
using OSRS.Application.Seed.Interface;

namespace OSRS.Application.Seed.Models.Output
{
    /// <summary>
    /// Base Response
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// Response Constructor
        /// </summary>
        public BaseResponse() { }
        /// <summary>
        /// Response Constructor
        /// </summary>
        /// <param name="success">Status</param>
        /// <param name="message">Message</param>
        public BaseResponse(bool success = false, string message = "")
        {
            Success = success;
            Message = message;
        }
        /// <summary>
        /// Response Status
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Response Message
        /// </summary>
        public string Message { get; set; }

    }
    /// <summary>
    /// Response
    /// </summary>
    public class Response : BaseResponse
    {
        /// <summary>
        /// Response Constructor
        /// </summary>
        public Response()
        { }
        /// <summary>
        /// Response Constructor
        /// </summary>
        /// <param name="success">Status</param>
        /// <param name="message">Message</param>
        /// <param name="actionCode">Action Code</param>
        public Response(bool success = false, string message = "", int? actionCode = -1) : base(success, message)
        {
            ActionCode = actionCode;
        }
        /// <summary>
        /// Action code
        /// </summary>
        public int? ActionCode { get; set; }
        /// <summary>
        /// Notification
        /// </summary>
        [DefaultValue(null)]
        public NotificationItemModel Notification { get; set; }
    }

    /// <summary>
    /// Response with Data
    /// </summary>
    /// <typeparam name="TData">Data</typeparam>
    public class Response<TData> : Response
    {
        /// <summary>
        /// Response Constructor
        /// </summary>
        public Response()
        {
        }
        /// <summary>
        /// Response Constructor
        /// </summary>
        /// <param name="success">Status</param>
        /// <param name="message">Message</param>
        /// <param name="data">Data</param>
        public Response(bool success = false, string message = "", TData data = default) : base(success, message)
        {
            Data = data;
        }
        /// <summary>
        /// Response Constructor
        /// </summary>
        /// <param name="success">Status</param>
        /// <param name="data">Data</param>
        public Response(bool success = false, TData data = default) : base(success)
        {
            Data = data;
        }
        /// <summary>
        /// Response Data
        /// </summary>
        public TData Data { get; set; }

    }

    /// <summary>
    /// Configuration Response
    /// </summary>
    /// <typeparam name="TData">Data</typeparam>
    public class ConfigurationResponse<TData> : Response<TData>, IConfigurationResponse
    {
        /// <summary>
        /// Configuration Constructor
        /// </summary>
        public ConfigurationResponse() { }
        /// <summary>
        /// Configuration Constructor
        /// </summary>
        /// <param name="success">Status</param>
        /// <param name="data">Data</param>
        public ConfigurationResponse(bool success = false, TData data = default) : base(success, data) { }
        /// <summary>
        /// Configuration Constructor
        /// </summary>
        /// <param name="success">Status</param>
        /// <param name="message">Message</param>
        /// <param name="data">Data</param>
        public ConfigurationResponse(bool success = false, string message = "", TData data = default) : base(success, message, data) { }
        /// <summary>
        /// Configuration
        /// </summary>
        public ConfigurationModel Configuration { get; set; }
    }
    
    /// <summary>
    /// Error Response
    /// </summary>
    public class ErrorResponse : Response
    {
        /// <summary>
        /// Error Response Constructor
        /// </summary>
        public ErrorResponse() : this("Erros Desconocido")
        {
            
        }
        /// <summary>
        /// Error Response Constructor
        /// </summary>
        /// <param name="message">Message</param>
        public ErrorResponse(string message) : base(false, message)
        {

        }
    }
    /// <summary>
    /// Error Response with Data
    /// </summary>
    /// <typeparam name="TData">Data</typeparam>
    public class ErrorResponse<TData> : Response<TData>
    {
        /// <summary>
        /// Error Constructor
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="data">Data</param>
        public ErrorResponse(string message, TData data = default) : base(false, message, data)
        {

        }
    }
}
