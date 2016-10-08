using BrewJournal.Tests.Testability.Hosting;
using Xunit;

namespace BrewJournal.Tests.FullStackTests
{
    public class IntegrationExampleTests
    {
        private readonly AppHost _appHost;

        public IntegrationExampleTests()
        {
            _appHost = AppHost.Simulate("BrewJournal");
        }

        [Fact(Skip = "Effects actual data, not sure this is the way to go")]
        public void LogInProcess()
        {
            var addBrewUrl = "/Brew/Add";

            _appHost.Start(browsingSession =>
            {
                var addBrewResult = browsingSession.Post(addBrewUrl, new
                {
                    Name = "Existing Brew"
                });

                var addBrewResponse = addBrewResult.Response;
            });
        }
    }
}