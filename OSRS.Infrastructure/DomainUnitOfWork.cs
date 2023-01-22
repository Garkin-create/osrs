using OSRS.Domain.Seed;
using OSRS.Domain.Seed.UnitOfWorks;
using OSRS.Infrastructure;
using OSRS.Infrastructure.Model.Domain;

namespace Stu.Cubatel.Infrastructure.Model.Domain
{
    public class DomainUnitOfWork : BaseUnitOfWork, IDomainUnitOfWork
    {
        public DomainUnitOfWork(DomainContext context, ISystemLogger logger) : base(context, logger)
        {
        }
    }
}
