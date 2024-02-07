using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Radio.Model.JwtTokenConfig;
using Radio.Model.ResponseModel.User;

namespace Radio.Services.GeneratorTokenServices;

public interface IGeneratorTokenServices
{
    public string Generator(string login);
}

public class GeneratorTokenServices : IGeneratorTokenServices
{
    public string Generator(string login)
    {
        var clams = Claims(login);
        JwtTokenConfig jwtTokenConfig = new JwtTokenConfig();
        DateTime dateTime = DateTime.UtcNow;

        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer: jwtTokenConfig.Issuer,
            audience: jwtTokenConfig.Audience,
            notBefore: dateTime,
            claims: clams,
            expires: dateTime.Add(TimeSpan.FromDays(jwtTokenConfig.RefreshTokenExpiration)),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenConfig.Secret)), SecurityAlgorithms.HmacSha256)
        );

        string jwtSecurityTokenHandler = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);


        var response = new
        {
            // token = jwtSecurityTokenHandler,
            // loginUser = data.Login,
            // FullName = data.FullName,
            // role = data.Role
        };

        return response.ToString();
    }

    private static List<Claim> Claims(string login)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, login),
            // new Claim(ClaimsIdentity.DefaultRoleClaimType, Roles()),
        };

        return claims;
    }

//     private static GetUser Roles(string name)
//     {
//         AdminPanelServices.UserServices userServices = new AdminPanelServices.UserServices();
//         foreach (var data in userServices.GetName(name))
//         {
//             
//         }
//     }
}