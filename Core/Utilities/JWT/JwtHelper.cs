using Core.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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

        public AccessToken CreateToken(BaseUser user)
        {
            //Özellikler oku ve token'i yaz
            DateTime expirationTime = DateTime.Now.AddMinutes(_tokenOptions.ExpirationTime);
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));

            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer : _tokenOptions.Issuer, 
                audience: _tokenOptions.Audience,
                claims: null,
                notBefore: DateTime.Now,
                expires: expirationTime,
                signingCredentials: signingCredentials
                );

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            string jwtToken = jwtSecurityTokenHandler.WriteToken(jwt);
            return new AccessToken() { Token = jwtToken, ExpirationTime = expirationTime};
        }
    }
}
