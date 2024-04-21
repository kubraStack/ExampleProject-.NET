using ValidationException = Core.CrossCuttingConcerns.Exceptions.Types.ValidationException;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.Application.Pipelines.Validation
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //Her command öncesi çalışacak kod
            //FluentValidation
            Console.WriteLine("Validation Çalıştı");

            ValidationContext<object> context = new ValidationContext<object>(request);

            var errors = _validators
                .Select(validator => validator.Validate(context))
                .SelectMany(result => result.Errors)
                //.GroupBy(keySelector: p=>p.PropertyName, resultSelector: (propertyName, errors) => new ValidationException())
                .ToList();

            if (errors.Any())
            {
                throw new ValidationException(errors.Select(e => e.ErrorMessage).ToList());
            }

            TResponse response = await next();
            return response;
        }
    }
}

