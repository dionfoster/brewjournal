using System;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;

namespace BrewJournal.Infrastructure
{
    public class ActionPerControllerFactory : DefaultControllerFactory
    {
        private readonly IContainer _container;

        public ActionPerControllerFactory(IContainer container)
        {
            _container = container;
        }

        protected override IController GetControllerInstance(RequestContext context, Type controllerType)
        {
            var featureController = GetActionEntityControllerType(context);

            var controller = (IController) _container.Resolve(featureController);

            return controller;
        }

        private static Type GetActionEntityControllerType(RequestContext context)
        {
            var routeData = context.RouteData;

            var controllerName = routeData.GetRequiredString("controller");
            var action = routeData.GetRequiredString("action");

            var fullyQualifiedType = $"BrewJournal.Features.{controllerName}.{action}{controllerName}Controller";

            var assembly = typeof(MvcApplication).Assembly;
            var featureController = assembly.GetType(fullyQualifiedType);

            if (featureController == null)
                throw new Exception($"The controller type '{fullyQualifiedType}' for path '{context.HttpContext.Request.Path}' could not be found.");

            return featureController;
        }
    }
}