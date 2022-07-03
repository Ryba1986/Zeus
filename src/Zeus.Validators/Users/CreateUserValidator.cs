using FluentValidation;
using Zeus.Models.Users.Commands;

namespace Zeus.Validators.Users
{
   public sealed class CreateUserValidator : AbstractValidator<CreateUserCommand>
   {
      public CreateUserValidator()
      {
         RuleFor(x => x.Name).NotEmpty().Length(3, 50);

         RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(50);

         RuleFor(x => x.Role).IsInEnum();
      }
   }
}
