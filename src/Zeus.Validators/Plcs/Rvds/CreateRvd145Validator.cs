using FluentValidation;
using Zeus.Models.Plcs.Rvds.Commands;

namespace Zeus.Validators.Plcs.Rvds
{
   public sealed class CreateRvd145Validator : AbstractValidator<CreateRvd145Command>
   {
      public CreateRvd145Validator()
      {
         RuleFor(x => x.Date).NotEmpty();

         RuleFor(x => x.DeviceId).NotEmpty();
      }
   }
}
