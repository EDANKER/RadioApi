namespace Radio.Model.JwtTokenConfig;

public class JwtTokenConfig
{
    public const string Secret = "F-JaNdRfUserjd89#5*6Xn2r5usErw8x/A?D(G+KbPeShV";
    public string Issuer = "https://localhost:5135/";
    public string Audience = "https://localhost:5135/";
    public int RefreshTokenExpiration = 7;
}