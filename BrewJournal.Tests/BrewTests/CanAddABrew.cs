using System.Data.Entity;
using System.Threading.Tasks;
using BrewJournal.Features.Brew;
using Shouldly;

namespace BrewJournal.Tests.BrewTests
{
    public class CanAddABrew : SubcutaneousMvcTest<AddBrewController>
    {
        private AddBrewViewModel _addBrewViewModel;

        public void GivenAValidBrewEntry()
        {
            _addBrewViewModel = new AddBrewViewModel {Name = "A new brew"};
        }

        public void WhenANewBrewIsCreated()
        {
            ExecuteControllerAction(c => c.Add(_addBrewViewModel));
        }

        public async Task ThenItIsStoredForBrewProcessesAndObservations()
        {
            var brew = await VerifyDbContext.Brews.SingleAsync();

            brew.Name.ShouldBe("A new brew");
        }
    }
}