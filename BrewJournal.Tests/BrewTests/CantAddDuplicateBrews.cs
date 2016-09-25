using System.Data.Entity;
using System.Threading.Tasks;
using BrewJournal.Domain;
using BrewJournal.Features.Brew;
using Shouldly;
using TestStack.BDDfy;

namespace BrewJournal.Tests.BrewTests
{
    [Story(
        AsA = "As a brewer",
        IWant = "I want to be able to add unique brews only",
        SoThat = "So that I dont get confused about brew names")]
    public class CantAddDuplicateBrews : SubcutaneousMvcTest<AddBrewController>
    {
        private string _existingBrew = "Existing brew";

        public async Task GivenABrewExists()
        {
            SeedDbContext.Brews.Add(new Brew(_existingBrew));

            await SeedDbContext.SaveChangesAsync();
        }

        public void WhenANewBrewIsCreated()
        {
            ExecuteControllerAction(c => c.Add(new AddBrewViewModel {Name = _existingBrew}));
        }

        public async Task ThenOnlyOneBrewExists()
        {
            var brewCount = await VerifyDbContext.Brews.CountAsync(x => x.Name == _existingBrew);

            brewCount.ShouldBe(1);
        }
    }
}