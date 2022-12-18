using Microsoft.EntityFrameworkCore;

namespace OSRS.Persistance.Db
{
    public class OSRSReadOnlyDbContext : AppDbContext
    {
        public OSRSReadOnlyDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
