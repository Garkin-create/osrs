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
    public class AlchemyRepository: BaseEntityRepository<AlchemyObject>, IAlchemyEntityRepository
    {

        public AlchemyRepository(DbContext dbContext, ISystemLogger logger) : base(dbContext, logger)
        {
        }

        
    }

    public interface IAlchemyEntityRepository: IEntityRepository<AlchemyObject>
    {
        
    }

}