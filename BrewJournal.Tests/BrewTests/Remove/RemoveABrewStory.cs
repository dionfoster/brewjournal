using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BrewJournal.Domain;
using BrewJournal.Features.Brew;
using BrewJournal.Tests.Testability;
using Shouldly;
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
    }

    public class CanRemoveABrew : SubcutaneousMvcTest<RemoveBrewController>
    {
        public Guid IdOfExistingBrew { get; private set; }

        public async Task GivenACrapBrew()
        {
            SeedDbContext.Brews.Add(new Brew("crap brew"));

            await SeedDbContext.SaveChangesAsync();

            IdOfExistingBrew = SeedDbContext.Brews.First().Id;
        }

        public void WhenRemovingTheCrapBrew()
        {
            ExecuteControllerAction(c => c.Remove(IdOfExistingBrew));
        }

        public async Task ThenItShouldNoLongerBePresent()
        {
            var brewCount = await VerifyDbContext.Brews.CountAsync(x => x.Id == IdOfExistingBrew);

            brewCount.ShouldBe(0);
        }
    }
}