namespace OSRS.Application.Seed.Input
{
    /// <summary>
    /// Client Base Input Model
    /// </summary>
    public abstract class ClientAppRequiredInput
    {
        /// <summary>
        /// Client Id
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// Secret Hash
        /// </summary>
        public string Hash { get; set; }
    }
}
