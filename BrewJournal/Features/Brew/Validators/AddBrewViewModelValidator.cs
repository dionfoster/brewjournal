using System.Linq;
using BrewJournal.EF;
using FluentValidation;

namespace BrewJournal.Features.Brew.Validators
{
    public class AddBrewViewModelValidator : AbstractValidator<AddBrewViewModel>
    {
        private readonly BrewContext _brewContext;

        public AddBrewViewModelValidator(BrewContext brewContext)
        {
            _brewContext = brewContext;

            RuleFor(x => x.Name)
                .Must(BeAUniqueName)
                .WithMessage("That brew name is already taken; please try another.");
        }

        public bool BeAUniqueName(string name)
        {
            return !_brewContext.Brews.Any(x => x.Name == name);
        }
    }
}