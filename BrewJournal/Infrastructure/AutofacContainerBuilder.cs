using Autofac;
using Autofac.Integration.Mvc;

namespace BrewJournal.Infrastructure
{
    public class AutofacContainerBuilder
    {
        public IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            return builder.Build();
        }
    }
}