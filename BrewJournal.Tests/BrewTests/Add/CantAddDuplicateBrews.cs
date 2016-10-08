using System.Data.Entity;
using System.Threading.Tasks;
using BrewJournal.Domain;
using BrewJournal.Features.Brew;
using BrewJournal.Tests.Testability;
using Shouldly;

namespace BrewJournal.Tests.BrewTests.Add
{
    public class CantAddDuplicateBrews : SubcutaneousMvcTest<AddBrewController>
    {
        private readonly string _existingBrew = "Existing brew";

        public async Task GivenABrewExists()
        {
            SeedDbContext.Brews.Add(new Brew(_existingBrew));

            await SeedDbContext.SaveChangesAsync();
        }

        public void WhenANewBrewIsCreated()
        {
            ExecuteControllerActionWithInvalidState(c => c.Add(new AddBrewViewModel {Name = _existingBrew}));
        }

        public async Task ThenOnlyOneBrewExists()
        {
            var brewCount = await VerifyDbContext.Brews.CountAsync(x => x.Name == _existingBrew);

            brewCount.ShouldBe(1);
        }
    }
}