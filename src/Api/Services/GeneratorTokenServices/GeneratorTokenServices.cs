using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Api.Model.JwtTokenConfig;
using Api.Model.ResponseModel.User;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.IO;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace Radio.Services.GeneratorTokenServices;

public interface IGeneratorTokenServices
{
    public string Generator(string login);
}

public class GeneratorTokenServices : IGeneratorTokenServices
{
    public string Generator(string login)
    {
        JwtTokenConfig jwtTokenConfig = new JwtTokenConfig();
        DateTime dateTime = DateTime.Now;

        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer: JwtTokenConfig.Issuer,
            audience: JwtTokenConfig.Audience,
            notBefore: dateTime,
            claims: Claims(login),
            expires: dateTime.Add(TimeSpan.FromDays(JwtTokenConfig.RefreshTokenExpiration)),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenConfig.Secret)), SecurityAlgorithms.HmacSha256)
        );

        string jwtSecurityTokenHandler = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        
        var response = new
        {
            token = jwtSecurityTokenHandler,
        };

        return JsonConvert.SerializeObject(response);
    }

    private static List<Claim> Claims(string login)
    {
        List<Claim> claims =
        [
            new Claim(ClaimsIdentity.DefaultNameClaimType, login)
        ];

        return claims;
    }
}