using Google.Apis.Auth;
using Internet_Market_WebApi.Data.Entities.Identity;

namespace Internet_Market_WebApi.Abstract
{
    public interface IJwtTokenService
    {
        Task<string> CreateToken(UserEntity user);
        string GetEmailFromToken(string token);
        Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(string tokenId);

    }
}
