using Microsoft.EntityFrameworkCore;

namespace OSRS.Persistance.Db
{
    public class OSRSWriteDbContext : AppDbContext
    {
        public OSRSWriteDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public OSRSWriteDbContext() { }
    }
}