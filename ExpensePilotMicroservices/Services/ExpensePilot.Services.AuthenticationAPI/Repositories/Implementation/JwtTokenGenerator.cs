using ExpensePilot.Services.AuthenticationAPI.Models.Domain;
using ExpensePilot.Services.AuthenticationAPI.Repositories.Implementation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpensePilot.Services.AuthenticationAPI.Repositories.Interface
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions jwtOptions;

        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            this.jwtOptions = jwtOptions.Value;
        }
        public string GenerateToken(User user, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            //SecretKey
            var key = Encoding.ASCII.GetBytes(jwtOptions.Secret);

            //Claims - can store email id, like a key value pair
            var claimList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), //Converts guid to string for claims
                new Claim(JwtRegisteredClaimNames.Name, user.UserName.ToString())
            };

            claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            //config properties for a token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = jwtOptions.Audience,
                Issuer = jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddDays(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)//default algo standard
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
