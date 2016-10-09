using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ApprovalUtilities.Utilities;
using Autofac;
using BrewJournal.EF;
using FluentValidation;
using Http.TestLibrary;
using TestStack.FluentMVCTesting;

namespace BrewJournal.Tests.Testability
{
    public abstract class SubcutaneousMvcTest<TController> : IDisposable
        where TController : Controller
    {
        private readonly DatabaseFixture _databaseFixture;
        private readonly HttpSimulator _httpRequest;
        private readonly ILifetimeScope _lifetimeScope;

        protected TController Controller { get; }
        protected ControllerResultTest<TController> ActionResult { get; set; }
        protected BrewContext SeedDbContext => _databaseFixture.SeedDbContext;
        protected BrewContext VerifyDbContext => _databaseFixture.VerifyDbContext;

        protected SubcutaneousMvcTest()
        {
            _databaseFixture = new DatabaseFixture();
            _lifetimeScope = ContainerFixture.GetTestLifetimeScope(cb =>
                cb.Register(_ => _databaseFixture.WorkDbContext)
                    .AsSelf()
                    .AsImplementedInterfaces()
                    .InstancePerTestRun());

            _httpRequest = new HttpSimulator().SimulateRequest();

            var routes = new RouteCollection();

            RouteConfig.RegisterRoutes(routes);

            Controller = BuildController(routes);
        }

        TController BuildController(RouteCollection routes)
        {
            var controller = _lifetimeScope.Resolve<TController>();

            var httpContext = new HttpContextWrapper(HttpContext.Current);

            controller.ControllerContext = new ControllerContext(httpContext, new RouteData(), controller);
            controller.Url = new UrlHelper(controller.Request.RequestContext, routes);

            return controller;
        }

        protected void ExecuteControllerAction(Expression<Func<TController, Task<ActionResult>>> action)
        {
            ValidateControllerAction((MethodCallExpression)action.Body);

            ActionResult = Controller.WithCallTo(action);
        }

        protected void ExecuteControllerAction(Expression<Func<TController, ActionResult>> action)
        {
            ValidateControllerAction((MethodCallExpression)action.Body);

            ActionResult = Controller.WithCallTo(action);
        }

        private void ValidateControllerAction(MethodCallExpression methodCallExpression)
        {
            var controllerActionParameters = GetExpressionParameters(methodCallExpression);

            foreach (var parameter in controllerActionParameters)
            {
                var validator = GetValidatorForParameter(parameter);

                var validationResult = validator?.Validate(parameter);

                validationResult?.Errors?
                    .ForEach(x => Controller.ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
            }
        }

        private static IEnumerable<object> GetExpressionParameters(MethodCallExpression methodCallExpression)
        {
            var expressionArguments = methodCallExpression.Arguments;

            var controllerActionParameters = expressionArguments
                .Select(x => Expression.Convert(x, typeof(object))) // box the argument types
                .Select(x => Expression.Lambda<Func<object>>(x, null).Compile()()) // then compile them to get the actual value
                .ToList();

            return controllerActionParameters;
        }

        private IValidator GetValidatorForParameter(object parameter)
        {
            var parameterType = parameter.GetType();
            var genericType = typeof(IValidator<>).MakeGenericType(parameterType);

            if (_lifetimeScope.IsRegistered(genericType))
            {
                return (IValidator)_lifetimeScope.Resolve(genericType);
            }

            return null;
        }

        protected TDependency Resolve<TDependency>()
        {
            return _lifetimeScope.Resolve<TDependency>();
        }

        public void Dispose()
        {
            _databaseFixture.Dispose();
            _httpRequest.Dispose();
            _lifetimeScope.Dispose();
        }
    }
}