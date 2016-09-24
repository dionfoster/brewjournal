using System;
using System.Web;
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
            try
            {
                var controller = (IController)_container.Resolve(controllerType);

                return controller;
            }
            catch (Exception)
            {
                throw new HttpException(404, $"The controller for path '{context.HttpContext.Request.Path}' could not be found.");
            }
        }
    }
}