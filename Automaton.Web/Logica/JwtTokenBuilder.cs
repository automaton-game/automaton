using System;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Automaton.Web.Logica
{
    public class JwtTokenBuilder
    {
        public static string SECRET = "un secreto algo mas largo que el anterior";

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(SECRET));
            return securityKey;
        }

        public string GenerateTokenJwt(string username)
        {
            var securityKey = GetSymmetricSecurityKey();
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // create a claimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) });

            // create token to the user
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                subject: claimsIdentity,
                signingCredentials: signingCredentials);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            return jwtTokenString;
        }
    }
}
