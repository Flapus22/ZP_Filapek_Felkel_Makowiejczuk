using Microsoft.AspNetCore.Mvc;
using ZP_Filapek_Felkel_Makowiejczuk.Dto;
using ZP_Filapek_Felkel_Makowiejczuk.Interface.Authentication;

namespace ZP_Filapek_Felkel_Makowiejczuk.Controllers;

[Route("test/acount")]
[ApiController]
public class AccountController : Controller
{
    private readonly IAccountService accountService;

    public AccountController(IAccountService accountService)
    {
        this.accountService = accountService;
    }

    [HttpPost("register")]
    public IActionResult RegisterUser([FromBody] RegisterUser registerUser)
    {
        accountService.RegisterUser(registerUser);
        return Ok();
    }

    [HttpPost("login")]
    public IActionResult LoginUser([FromBody] Login loginUser)
    {
        var token = accountService.Login(loginUser);
        if (token != null)
        {
            return Ok(new { Token = token });
        }
        else
        {
            return Unauthorized();
        }
    }

}
