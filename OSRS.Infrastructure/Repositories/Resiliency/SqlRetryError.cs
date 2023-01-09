namespace OSRS.Infrastructure.Repositories.Resiliency
{
    public class SqlRetryError
    {
        public const int Timeout = -2;
        public const int Deadlock = 1205;
        public const int OpenConnectionFail = 53;
        public const int TransportFail = 121;
        public const int NetworkProblem = 1231;
    }
}
