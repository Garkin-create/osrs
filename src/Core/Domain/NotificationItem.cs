using OSRS.Domain;

namespace Stu.Cubatel.Domain.Entities.ValueObjects
{
    public class NotificationItem
    {
        public NotificationItem()
        {

        }

        public NotificationItem(MessageType messageType, string message)
        {
            MessageType = messageType;
            Message = message;
        }

        public MessageType MessageType { get; set; } = MessageType.Info;
        public string Message { get; set; } = string.Empty;
    }
}
