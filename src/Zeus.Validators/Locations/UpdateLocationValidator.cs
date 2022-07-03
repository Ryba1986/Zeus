using FluentValidation;
using Zeus.Models.Locations.Commands;
using Zeus.Validators.Base;

namespace Zeus.Validators.Locations
{
   public sealed class UpdateLocationValidator : AbstractValidator<UpdateLocationCommand>
   {
      public UpdateLocationValidator()
      {
         RuleFor(x => x.Id).NotEmpty();

         RuleFor(x => x.Name).NotEmpty().Length(3, 30);

         RuleFor(x => x.MacAddress).NotEmpty().Matches(LocationRules.MacAddressFormat);

         RuleFor(x => x.Version).NotEmpty();
      }
   }
}
