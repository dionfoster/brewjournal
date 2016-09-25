using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using BrewJournal.Infrastructure;

namespace BrewJournal
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var container = AutofacContainerBuilder.Build();

            SetControllerFactoryToActionPerControllerFactory(container);

            SetViewEngineToFeatureFolderStructure();
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
    }
}