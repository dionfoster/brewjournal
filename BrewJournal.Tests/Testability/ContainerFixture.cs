using System;
using Autofac;
using Autofac.Builder;
using Autofac.Core.Lifetime;
using BrewJournal.Infrastructure;

namespace BrewJournal.Tests.Testability
{
    public static class ContainerFixture
    {
        private static readonly IContainer Container;

        static ContainerFixture()
        {
            Container = AutofacContainerBuilder.Build();

            AppDomain.CurrentDomain.DomainUnload += (sender, args) => Container.Dispose();
        }

        public static ILifetimeScope GetTestLifetimeScope(Action<ContainerBuilder> modifier = null)
        {
            return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag, cb =>
            {
                ExternalMocks(cb);
                modifier?.Invoke(cb);
            });
        }

        private static void ExternalMocks(ContainerBuilder cb)
        {
            cb.Register(_ => new StaticDateTimeProvider(DateTimeOffset.UtcNow.AddMinutes(1)))
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerTestRun();
        }
    }

    public static class RegistrationExtensions
    {
        // This extension method makes the registrations in the ExternalMocks method clearer in intent - I create a HTTP request lifetime around each test since I'm using my container in a web app
        public static IRegistrationBuilder<TLimit, TActivatorData, TStyle> InstancePerTestRun
            <TLimit, TActivatorData, TStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TStyle> registration,
                params object[] lifetimeScopeTags)
        {
            return registration.InstancePerRequest(lifetimeScopeTags);
        }
    }
}