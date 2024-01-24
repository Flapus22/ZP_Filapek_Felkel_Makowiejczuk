using Data;
using FluentValidation;
using ZP_Filapek_Felkel_Makowiejczuk.Dto;

namespace ZP_Filapek_Felkel_Makowiejczuk.Validator.Authentication;

public class UpdateUserDataValidator : AbstractValidator<RegisterUser>
{
    public UpdateUserDataValidator(DBContext db)
    {
        
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);

        RuleFor(x => x.Email).Custom((value, context) =>
        {
            var emailInUse = db.Users.Any(x => x.Email == value);
            if (emailInUse)
            {
                context.AddFailure("Email", "That email is taken");
            }
        });
    }
}
