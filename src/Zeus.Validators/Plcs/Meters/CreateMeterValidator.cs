using FluentValidation;
using Zeus.Models.Plcs.Meters.Commands;

namespace Zeus.Validators.Plcs.Meters
{
   public sealed class CreateMeterValidator : AbstractValidator<CreateMeterCommand>
   {
      public CreateMeterValidator()
      {
         RuleFor(x => x.Date).NotEmpty();

         RuleFor(x => x.DeviceId).NotEmpty();
      }
   }
}
