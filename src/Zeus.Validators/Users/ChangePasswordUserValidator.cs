using FluentValidation;
using Zeus.Models.Users.Commands;

namespace Zeus.Validators.Users
{
   public sealed class ChangePasswordUserValidator : AbstractValidator<ChangePasswordUserCommand>
   {
      public ChangePasswordUserValidator()
      {
         RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(50);

         RuleFor(x => x.OldPassword).NotEmpty().Length(10, 30);

         RuleFor(x => x.NewPassword).NotEmpty().Length(10, 30);
      }
   }
}
