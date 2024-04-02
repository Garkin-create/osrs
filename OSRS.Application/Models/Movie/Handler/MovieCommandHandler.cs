using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using OSRS.Application.Models.Movie.Command;
using OSRS.Application.Seed.Command;
using OSRS.Application.Seed.Models.Output;
using OSRS.Domain.Entities.Movie;
using OSRS.Domain.Seed;
using OSRS.Domain.Seed.UnitOfWorks;
using OSRS.Infrastructure.Repositories;
using OSRS.Resources.Localization;

namespace OSRS.Application.Models.Movie.Handler
{
    public class MovieCommandHandler: ICommandHandler<PushMovieCommand, Response>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IDomainUnitOfWork _domainUnitOfWork;
        private readonly IMapper _mapper;

        public MovieCommandHandler(IMovieRepository movieRepository, IDomainUnitOfWork domainUnitOfWork, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _domainUnitOfWork = domainUnitOfWork;
            _mapper = mapper;
        }
        
        public async Task<Response> Handle(PushMovieCommand request, CancellationToken cancellationToken)
        {
            Response result;
            MovieObject movie = _mapper.Map<MovieObject>(request.Model);
            try
            {
                if ((await _movieRepository.AddMovie(movie, cancellationToken)).Result
                    != OperationResultType.Success 
                    || !await _domainUnitOfWork.CommitAsync(cancellationToken, nameof(PushMovieCommand)))
                    result = new ErrorResponse("Error");
                else
                    result = new(true, "Successful");
            }
            catch (Exception exc)
            {
                result = new ErrorResponse("Error");
            }

            return result;
        }
    }
}