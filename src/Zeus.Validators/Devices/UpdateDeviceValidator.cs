using FluentValidation;
using Zeus.Models.Devices.Commands;

namespace Zeus.Validators.Devices
{
   public sealed class UpdateDeviceValidator : AbstractValidator<UpdateDeviceCommand>
   {
      public UpdateDeviceValidator()
      {
         RuleFor(x => x.Id).NotEmpty();

         RuleFor(x => x.LocationId).NotEmpty();

         RuleFor(x => x.Name).NotEmpty().Length(3, 30);

         RuleFor(x => x.Type).IsInEnum();

         RuleFor(x => x.RsBoundRate).IsInEnum();

         RuleFor(x => x.RsDataBits).IsInEnum();

         RuleFor(x => x.RsParity).IsInEnum();

         RuleFor(x => x.RsStopBits).IsInEnum();

         RuleFor(x => x.Version).NotEmpty();
      }
   }
}
