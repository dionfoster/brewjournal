using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using BrewJournal.EF;
using Http.TestLibrary;
using TestStack.BDDfy;
using TestStack.FluentMVCTesting;
using Xunit;

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

            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            _httpRequest = new HttpSimulator().SimulateRequest();

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
            ActionResult = Controller.WithCallTo(action);
        }

        protected void ExecuteControllerAction(Expression<Func<TController, ActionResult>> action)
        {
            ActionResult = Controller.WithCallTo(action);
        }

        protected void ExecuteControllerActionWithInvalidState(Expression<Func<TController, ActionResult>> action)
        {
            Controller.WithModelErrors();

            ActionResult = Controller.WithCallTo(action);
        }

        [Fact]
        public virtual void ExecuteScenario()
        {
            this.BDDfy();
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