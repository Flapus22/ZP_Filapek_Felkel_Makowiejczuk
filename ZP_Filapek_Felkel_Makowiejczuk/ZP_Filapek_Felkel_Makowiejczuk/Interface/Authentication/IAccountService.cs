using Microsoft.AspNetCore.Identity.Data;
using ZP_Filapek_Felkel_Makowiejczuk.Dto;

namespace ZP_Filapek_Felkel_Makowiejczuk.Interface.Authentication;

public interface IAccountService
{
    public void RegisterUser(RegisterUser registerUser);
    public string Login(Login login);
}
