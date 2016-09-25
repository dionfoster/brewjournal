using System;
using System.Web.Mvc;
using FluentValidation;

namespace BrewJournal.Infrastructure.Validation
{
    public class AutofacValidatorFactory : IValidatorFactory
    {
        public IValidator<T> GetValidator<T>()
        {
            return (IValidator<T>)GetValidator(typeof(T));
        }

        public IValidator GetValidator(Type type)
        {
            var genericType = typeof(IValidator<>).MakeGenericType(type);

            try
            {
                return (IValidator)DependencyResolver.Current.GetService(genericType);
            }
            catch (Exception) { }

            return null;
        }
    }
}