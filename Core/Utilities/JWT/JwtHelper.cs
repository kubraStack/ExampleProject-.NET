using Core.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.JWT
{
    public class JwtHelper : ITokenHelper
    {
        private readonly TokenOptions _tokenOptions;

        public JwtHelper(TokenOptions tokenOptions)
        {
            _tokenOptions = tokenOptions;
        }

        public AccessToken CreateToken(BaseUser user, List<OperationClaim> operationClaims)
        {
            //Özellikler oku ve token'i yaz
            DateTime expirationTime = DateTime.Now.AddMinutes(_tokenOptions.ExpirationTime);
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));

            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer : _tokenOptions.Issuer, 
                audience: _tokenOptions.Audience,
                claims: SettAllClaims(user, operationClaims.Select(i=>i.Name).ToList()),
                notBefore: DateTime.Now,
                expires: expirationTime,
                signingCredentials: signingCredentials
                );

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            string jwtToken = jwtSecurityTokenHandler.WriteToken(jwt);
            return new AccessToken() { Token = jwtToken, ExpirationTime = expirationTime};
        }

        protected IEnumerable<Claim> SettAllClaims(BaseUser user, List<string> operationClaims)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.FirstName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach (var operationClaim in operationClaims)
            {
                claims.Add(new Claim(ClaimTypes.Role, operationClaim));
            }
            claims.Add(new Claim("Tobeto", "abc"));//Token içine istediğimiz alanı istediğimiz değerle yazabiliriz.
            return claims;
        }
    }
}
