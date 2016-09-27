using Xunit;
using AppHost = BrewJournal.Tests.Testability.Hosting.AppHost;

namespace BrewJournal.Tests
{
    public class IntegrationExampleTests
    {
        private readonly AppHost appHost;

        public IntegrationExampleTests()
        {
            appHost = AppHost.Simulate("BrewJournal");
        }

        [Fact]
        public void LogInProcess()
        {
            var addBrewUrl = "/Brew/Add";

            appHost.Start(browsingSession =>
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