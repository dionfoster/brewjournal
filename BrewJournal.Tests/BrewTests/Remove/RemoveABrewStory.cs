using System.Linq;
using TestStack.BDDfy;
using Xunit;

namespace BrewJournal.Tests.BrewTests.Remove
{
    [Story(
        AsA = "As a brewer",
        IWant = "I want to remove a brew",
        SoThat = "So that I can get rid of crap ones")]
    public class RemoveABrewStory
    {
        [Fact]
        public void ShouldBeAbleToRemoveABrew()
        {
            new CanRemoveABrew().BDDfy();
        }

        [Fact]
        public void CanOnlyRemoveABrewThatExists()
        {
            new CanOnlyRemoveABrewThatExists().BDDfy();
        }
    }
}