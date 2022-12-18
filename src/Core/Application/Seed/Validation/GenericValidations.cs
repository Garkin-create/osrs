using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace Stu.Cubatel.Application.Seed.Validation
{
    public abstract class GenericValidations<T> : AbstractValidator<T>
    {
        private const string _passwordExpression = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";

        private readonly DateTime _minBirthdayDate = DateTime.Today.AddYears(-100);
        private readonly DateTime _maxBirthdayDate = DateTime.Today.AddYears(-10);
        private readonly IServiceProvider _serviceProvider;

       
        protected bool IsValidUserSession { get; init; }

        protected GenericValidations(IServiceProvider serviceProvider = null) 
            => _serviceProvider = serviceProvider;

        protected IRuleBuilderOptions<T, string> IsNotEmpty(Expression<Func<T, string>> expression, string message) 
            => RuleFor(expression)
                .NotEmpty().WithMessage(message);

        protected IRuleBuilderOptions<T, IList> IsNotEmptyList(Expression<Func<T, IList>> expression, string message) 
            => RuleFor(expression)
                .Must(collection => collection is { Count: > 0 }).WithMessage(message);

        protected IRuleBuilderOptions<T, string> Length(Expression<Func<T, string>> expression, string message, 
            int min, int max) 
            => RuleFor(expression)
                .Length(min, max).WithMessage(message);

        protected IRuleBuilderOptions<T, object> IsNotNull(Expression<Func<T, object>> expression, string message) 
            => RuleFor(expression)
                .Must((instance) => instance != null).WithMessage(message);
    }
}
