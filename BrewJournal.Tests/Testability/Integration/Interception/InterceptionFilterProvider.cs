using System.Collections.Generic;
using System.Web.Mvc;

namespace BrewJournal.Tests.Testability.Integration.Interception
{
    internal class InterceptionFilterProvider : IFilterProvider
    {
        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            yield return new Filter(new InterceptionFilter(), FilterScope.Action, null);
        }
    }
}