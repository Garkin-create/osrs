namespace OSRS.Application.Models.Seed.Output
{
    /// <summary>
    /// Custom Notification Item Model
    /// </summary>
    public class NotificationItemModel
    {
        /// <summary>
        /// Message Icon Type
        /// </summary>
        public int MessageType { get; set; }
        /// <summary>
        /// Custom Message Value
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Message Color
        /// </summary>
        public string Color { get; set; }
    }
}