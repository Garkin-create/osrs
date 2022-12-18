using OSRS.Api.Controllers.v1.Contracts.Requests;
using FluentValidation;

namespace OSRS.Api.Controllers.v1.Contracts.Validators
{
    public class AddContractsRequestValidator: AbstractValidator<AddContractsRequest>
    {
        public AddContractsRequestValidator()
        {
            RuleFor(x => x.Data).NotEmpty().WithMessage("La Lista de elementos no puede estar vacía");
            RuleFor(x => x.IdAgente).NotNull().NotEmpty().WithMessage("IdAgente es requerido");
            RuleFor(x => x.IdLabel).NotNull().NotEmpty().WithMessage("IdLabel es requerido");
        }
    }
}