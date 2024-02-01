using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Radio.Data.LdapConnect;
using Radio.Model.JwtTokenConfig;

namespace Radio.Services.GeneratorTokenServices;

public interface IGeneratorTokenServices
{
    public string Generator(string login, string password);
}

public class GeneratorTokenServices : IGeneratorTokenServices
{
    public string Generator(string login, string password)
    {
        var identity = GetIdentity(login, password);
        JwtTokenConfig jwtTokenConfig = new JwtTokenConfig();
        DateTime dateTime = DateTime.UtcNow;

        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer: jwtTokenConfig.Issuer, 
            audience: jwtTokenConfig.Audience, 
            notBefore: dateTime, 
            claims: identity.Claims,
            expires: dateTime.AddDays(jwtTokenConfig.RefreshTokenExpiration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenConfig.Secret)), SecurityAlgorithms.HmacSha256)
            );

        string jwtSecurityTokenHandler = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        var response = new
        {
            access_token = jwtSecurityTokenHandler,
            login = identity.Name
        };

        return response.ToString();
    }

    private ClaimsIdentity GetIdentity(string login, string password)
    {
        Task<bool> ldapConnectService = new LdapConnectService()
            .Validation(login, password);
        if (ldapConnectService.Result)
        {
            return null;
        }

        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, "Game"),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, "Admin"),
        };

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims.ToString(), "token", ClaimsIdentity.DefaultNameClaimType);

        return claimsIdentity;
    }
}