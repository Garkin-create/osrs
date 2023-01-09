using System;

namespace OSRS.Domain.Seed
{
    public class OperationResult
    {
        public OperationResult()
        {

        }
        public OperationResult(OperationResultType result, string message ="", ActionCodeType? actionCode = ActionCodeType.None)
        {
            Result = result;
            Message = message;
            ActionCode = actionCode;
        }

        public OperationResultType Result { get;}
        public string Message { get; } = string.Empty;
        public ActionCodeType? ActionCode { get; set; } 
    }

    public class OperationResult<T> : OperationResult
    {
        public OperationResult()
        {

        }
        public OperationResult(OperationResultType type, string message = "", ActionCodeType? actionCode = ActionCodeType.None) : base(type, message, actionCode)
        {
            Data = default;
        }
        public OperationResult(OperationResultType type, T data) : base(type)
        {
            Data = data;
        }
        public OperationResult(OperationResultType type, string message, T data, ActionCodeType? actionCode = ActionCodeType.None) : base(type, message, actionCode)
        {
            Data = data;
        }
        public OperationResult(OperationResult source) : base(source.Result, source.Message)
        {

        }
        public OperationResult(OperationResult source, T data) : this(source.Result, source.Message, data)
        {

        }
        public T Data { get; protected set; }


        public static implicit operator OperationResult<T>(OperationResult<object> v)
        {
            if(v.Data is T d)
                return new(v.Result, v.Message, d, v.ActionCode);
            throw new InvalidCastException();
        }
        public static implicit operator OperationResult<object>(OperationResult<T> v)
        {
            return new(v.Result, v.Message, v.Data, v.ActionCode);
        }
    }

    // public class NotificationResult : OperationResult, INotificationResult
    // {
    //     public NotificationItem Notification { get; init; }
    // }
    public class NotificationResult<T> : OperationResult<T>, INotificationResult
    {
        public NotificationResult()
        {

        }
        public NotificationResult(OperationResultType type, string message = "", ActionCodeType? actionCode = ActionCodeType.None) : base(type, message, actionCode)
        {
            Data = default;
        }
        public NotificationResult(OperationResultType type, T data) : base(type)
        {
            Data = data;
        }
        public NotificationResult(OperationResultType type, string message, T data, ActionCodeType? actionCode = ActionCodeType.None) : base(type, message, actionCode)
        {
            Data = data;
        }
        public NotificationResult(OperationResult source) : base(source.Result, source.Message)
        {

        }
        public NotificationResult(OperationResult source, T data) : this(source.Result, source.Message, data)
        {

        }
        //public NotificationItem Notification { get; init; }
    }

    public enum OperationResultType
    {
        Error = 1,
        Warning = 2,
        Success = 3
    }

    public interface INotificationResult
    {
        // public NotificationItem Notification { get;  }
    }

   
}
