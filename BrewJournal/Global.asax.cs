using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using BrewJournal.Infrastructure;
using BrewJournal.Infrastructure.CustomMvcResolution;
using FluentValidation;
using FluentValidation.Mvc;

namespace BrewJournal
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var container = AutofacContainerBuilder.Build();

            SetAutofacDependecyResolverAsDefaultResolver(container);

            SetControllerFactoryToActionPerControllerFactory(container);

            SetViewEngineToFeatureFolderStructure();

            SetFluentValidationAsModelValidator(container);
        }

        private static void SetAutofacDependecyResolverAsDefaultResolver(IContainer container)
        {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private void SetControllerFactoryToActionPerControllerFactory(IContainer container)
        {
            var controllerFactory = new ActionPerControllerFactory(container);

            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        private static void SetViewEngineToFeatureFolderStructure()
        {
            ViewEngines.Engines.Clear();

            ViewEngines.Engines.Add(new FeatureViewLocationRazorViewEngine());
        }

        private static void SetFluentValidationAsModelValidator(IContainer container)
        {
            ModelValidatorProviders.Providers.Add(
                new FluentValidationModelValidatorProvider(container.Resolve<IValidatorFactory>())
                {
                    AddImplicitRequiredValidator = false
                });
        }
    }
}