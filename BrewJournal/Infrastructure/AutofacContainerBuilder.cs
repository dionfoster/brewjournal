using Autofac;
using Autofac.Integration.Mvc;
using BrewJournal.EF;

namespace BrewJournal.Infrastructure
{
    public class AutofacContainerBuilder
    {
        public IContainer Build()
        {
            var builder = new ContainerBuilder();

            builder.Register(x => new BrewContext())
                .InstancePerLifetimeScope();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            return builder.Build();
        }
    }
}