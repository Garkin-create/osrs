using System;
using System.Threading;
using System.Threading.Tasks;
using ecomerce.Persistance.Repositories;
using OSRS.Domain.Entities.Movie;
using OSRS.Domain.Seed;

namespace OSRS.Infrastructure.Repositories
{
    public class MovieRepository: BaseEntityRepository<MovieObject>, IMovieRepository
    {
        public MovieRepository(DomainContext context, ISystemLogger logger) : base(context, logger)
        {
        }

        public async Task<OperationResult> AddMovie(MovieObject movie, CancellationToken cancellationToken = default)
        {
            OperationResult answer  = new(OperationResultType.Error, "");
            try
            {
                if (await AddAsync(movie))
                {
                    answer = new OperationResult(OperationResultType.Success, "Success");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return answer;
        }
    }
    
    public interface IMovieRepository: IEntityRepository<MovieObject>
    {
        Task<OperationResult> AddMovie(MovieObject alchemy, CancellationToken cancellationToken = default);
    }
}