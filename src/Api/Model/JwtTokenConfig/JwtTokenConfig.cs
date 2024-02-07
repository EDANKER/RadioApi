namespace Radio.Model.JwtTokenConfig;

public class JwtTokenConfig
{
    public const string Secret = "F-JaNdRfUserjd89#5*6Xn2r5usErw8x/A?D(G+KbPeShV";
    public readonly string Issuer = "https://localhost:5135/";
    public readonly string Audience = "https://localhost:5135/";
    public readonly int RefreshTokenExpiration = 7;
}