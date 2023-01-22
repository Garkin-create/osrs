using System;
using System.Threading;
using System.Threading.Tasks;
using ecomerce.Persistance.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OSRS.Domain.Entities;
using OSRS.Domain.Entities.User;
using OSRS.Domain.Repositories;
using OSRS.Domain.Seed;

namespace OSRS.Infrastructure.Repositories
{
    public class AlchemyRepository: BaseEntityRepository<AlchemyObject>, IAlchemyRepository
    {
        public AlchemyRepository(DomainContext dbContext, ISystemLogger logger) : base(dbContext, logger)
        {
        }


        public async Task<bool> AddAlchemy(AlchemyObject alchemy, CancellationToken cancellationToken = default)
        {
            var result = false;
            try
            {
                result = await AddAsync(alchemy);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return result;
        }
    }

    public interface IAlchemyRepository: IEntityRepository<AlchemyObject>
    {
        public Task<bool> AddAlchemy(AlchemyObject alchemy, CancellationToken cancellationToken = default);
    }

}