using FluentValidation;
using Zeus.Models.Users.Commands;

namespace Zeus.Validators.Users
{
   public sealed class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
   {
      public UpdateUserValidator()
      {
         RuleFor(x => x.Id).NotEmpty();

         RuleFor(x => x.Name).NotEmpty().Length(3, 50);

         RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(50);

         RuleFor(x => x.Role).IsInEnum();

         RuleFor(x => x.Version).NotEmpty();
      }
   }
}
