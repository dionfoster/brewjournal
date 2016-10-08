using TestStack.BDDfy;
using Xunit;

namespace BrewJournal.Tests.BrewTests.Add
{
    [Story(
        AsA = "As a brewer",
        IWant = "I want to add a new brew",
        SoThat = "So that I can record my processes and recipes")]
    public class AddABrewStory
    {
        [Fact]
        public void ShouldBeAbleToAddABrew()
        {
            new CanAddABrew().BDDfy();
        }

        [Fact]
        public void CantAddDuplicateBrews()
        {
            new CantAddDuplicateBrews().BDDfy();
        }
    }
}