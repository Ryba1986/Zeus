using FluentValidation;
using Zeus.Models.Devices.Commands;

namespace Zeus.Validators.Devices
{
   public sealed class CreateDeviceValidator : AbstractValidator<CreateDeviceCommand>
   {
      public CreateDeviceValidator()
      {
         RuleFor(x => x.LocationId).NotEmpty();

         RuleFor(x => x.Name).NotEmpty().Length(3, 30);

         RuleFor(x => x.Type).IsInEnum();

         RuleFor(x => x.RsBoundRate).IsInEnum();

         RuleFor(x => x.RsDataBits).IsInEnum();

         RuleFor(x => x.RsParity).IsInEnum();

         RuleFor(x => x.RsStopBits).IsInEnum();
      }
   }
}
