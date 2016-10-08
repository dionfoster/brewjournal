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

        [Fact]
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