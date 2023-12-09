using Google.Apis.Auth;
using Internet_Market_WebApi.Abstract;
using Internet_Market_WebApi.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Internet_Market_WebApi.Services.Account
{
    public class JwtTokenService: IJwtTokenService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<UserEntity> _userManager;
        public JwtTokenService(IConfiguration config, UserManager<UserEntity> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        public async Task<string> CreateToken(UserEntity user)
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>()
            {
                new Claim("name", user.UserName),
                new Claim("email", user.Email)
            };

            foreach (var claim in roles)
            {
                claims.Add(new Claim("roles", claim));
            }
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<String>("JWTSecretKey")));
            var signinCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                signingCredentials: signinCredentials,
                expires: DateTime.Now.AddDays(10),
                claims: claims
            );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        /////TEST
        public string GetEmailFromToken(string token)
        {
            string secret = "Hello3384KLLKJ&(**^)(&alsdjflkajs8*TPHl;sadnflahjfoilsjdflajsdfo";
            var key = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);
            return claims.Claims.ToArray()[1].Value.ToString();
        }
        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(string tokenId)
        {
            string clientID = _config["GoogleAuthSettings:ClientId"];
            var setting = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { clientID }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(tokenId, setting);
            return payload;
        }
    }
}
