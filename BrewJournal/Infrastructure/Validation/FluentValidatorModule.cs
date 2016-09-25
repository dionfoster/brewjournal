using System.Linq;
using System.Reflection;
using Autofac;
using FluentValidation;
using Module = Autofac.Module;

namespace BrewJournal.Infrastructure.Validation
{
    public class FluentValidatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<AutofacValidatorFactory>().As<IValidatorFactory>().SingleInstance();

            var validators = AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly());
            
            validators.ToList().ForEach(v => builder.RegisterType(v.ValidatorType).As(v.InterfaceType).InstancePerDependency());
        }
    }
}