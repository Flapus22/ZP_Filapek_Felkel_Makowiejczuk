using Data;
using ZP_Filapek_Felkel_Makowiejczuk.Controllers;
using ZP_Filapek_Felkel_Makowiejczuk.Interface.Authentication;

namespace ZP_Filapek_Felkel_Makowiejczuk.Services.Authentication;

public class UserService : IUserService
{
    private readonly DBContext context;

    public UserService(DBContext context)
    {
        this.context = context;
    }

    public UpdateUserDataDto GetUserData(int userId)
    {
        var user = context.Users.Find(userId);

        if (user == null)
        {
            return null;
        }

        var userData = new UpdateUserDataDto()
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DateOfBirth = user.DateOfBirth,
            City = user.City,
            District = user.District,
            Street = user.Street,
            PostalCode = user.PostalCode,
            HouseNumber = user.HouseNumber,
            UserType = user.UserType
        };

        return userData;
    }

    public bool UpdateUserData(int userId, UpdateUserDataDto updateUserData)
    {
        var user = context.Users.Find(userId);

        if (user == null)
        {
            return false;
        }

        user.FirstName = updateUserData.FirstName;
        user.LastName = updateUserData.LastName;
        user.DateOfBirth = updateUserData.DateOfBirth;
        user.City = updateUserData.City;
        user.District = updateUserData.District;
        user.Street = updateUserData.Street;
        user.PostalCode = updateUserData.PostalCode;
        user.HouseNumber = updateUserData.HouseNumber;
        user.UserType = updateUserData.UserType;

        context.Users.Update(user);

        context.SaveChanges();
        return true;
    }
}