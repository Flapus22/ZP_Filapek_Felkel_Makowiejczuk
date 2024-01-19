namespace ZP_Filapek_Felkel_Makowiejczuk.Model.Authentication;

public class AuthenticationSettings
{
    public string JwtKey { get; set; }
    public string JwtIssuer { get; set; }
    public int JwtExpireDays { get; set; }

    public AuthenticationSettings()
    {

    }

    public AuthenticationSettings(string jwtKey, string jwtIssuer, int jwtExpireDays)
    {
        JwtKey = jwtKey;
        JwtIssuer = jwtIssuer;
        JwtExpireDays = jwtExpireDays;
    }
}