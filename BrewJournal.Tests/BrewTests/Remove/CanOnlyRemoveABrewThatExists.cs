using System;
using System.Data.Entity;
using System.Threading.Tasks;
using BrewJournal.Domain;
using BrewJournal.Features.Brew;
using BrewJournal.Tests.Testability;
using Shouldly;

namespace BrewJournal.Tests.BrewTests.Remove
{
    public class CanOnlyRemoveABrewThatExists : SubcutaneousMvcTest<RemoveBrewController>
    {
        public async Task GivenAnExistingBrew()
        {
            SeedDbContext.Brews.Add(new Brew("existing brew"));

            await SeedDbContext.SaveChangesAsync();
        }

        public void WhenRemovingABrewThatDoesntExist()
        {
            ExecuteControllerAction(c => c.Remove(Guid.Empty));
        }

        public async Task ThenExistingBrewWillStillBePresent()
        {
            var brewCount = await VerifyDbContext.Brews.CountAsync();

            brewCount.ShouldBe(1);
        }
    }
}