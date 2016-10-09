using System;
using Autofac;
using FluentValidation;

namespace BrewJournal.Infrastructure.Validation
{
    public class AutofacValidatorFactory : IValidatorFactory
    {
        private readonly IComponentContext _componentContext;

        public AutofacValidatorFactory(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public IValidator<T> GetValidator<T>()
        {
            return (IValidator<T>)GetValidator(typeof(T));
        }

        public IValidator GetValidator(Type type)
        {
            var genericType = typeof(IValidator<>).MakeGenericType(type);

            try
            {
                return (IValidator)_componentContext.Resolve(genericType);
            }
            catch (Exception) { }

            return null;
        }
    }
}