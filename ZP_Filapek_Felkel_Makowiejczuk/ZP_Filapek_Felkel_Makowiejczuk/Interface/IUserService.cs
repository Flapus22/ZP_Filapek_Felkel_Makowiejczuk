using ZP_Filapek_Felkel_Makowiejczuk.Controllers;

namespace ZP_Filapek_Felkel_Makowiejczuk.Interface.Authentication;

public interface IUserService
{
    UpdateUserDataDto GetUserData(int userId);
    bool UpdateUserData(int userId, UpdateUserDataDto updateUserData);

}