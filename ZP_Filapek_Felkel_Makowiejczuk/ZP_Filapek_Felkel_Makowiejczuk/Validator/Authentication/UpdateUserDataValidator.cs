using Data;
using FluentValidation;
using ZP_Filapek_Felkel_Makowiejczuk.Controllers;

namespace ZP_Filapek_Felkel_Makowiejczuk.Validator.Authentication;

public class UpdateUserDataValidator : AbstractValidator<UpdateUserDataDto>
{
    public UpdateUserDataValidator(DBContext db)
    {
        RuleFor(x => x.FirstName).NotEmpty().MinimumLength(3);
        RuleFor(x => x.LastName).NotEmpty().MinimumLength(3);
        RuleFor(x => x.City).NotEmpty();
        RuleFor(x => x.Street).NotEmpty();
        RuleFor(x => x.PostalCode).NotEmpty();
        RuleFor(x => x.HouseNumber).NotEmpty();
    }
}