using FluentValidation;
using Zeus.Models.Plcs.Climatixs.Commands;

namespace Zeus.Validators.Plcs.Climatixs
{
   public sealed class CreateClimatixValidator : AbstractValidator<CreateClimatixCommand>
   {
      public CreateClimatixValidator()
      {
         RuleFor(x => x.Date).NotEmpty();

         RuleFor(x => x.DeviceId).NotEmpty();
      }
   }
}
