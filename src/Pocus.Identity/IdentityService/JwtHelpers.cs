using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Pocus.Models;

namespace Pocus.Identity.IdentityService;

public static class JwtHelpers
{
    // Helper method to generate claims for a user
    private static IEnumerable<Claim> GetClaims(this UserDto userAccounts)
    {
        IEnumerable<Claim> claims = new Claim[]
        {
            new("email", userAccounts.Email),
            new("id", userAccounts.Id.ToString()),
            new("tag_name", userAccounts.Tag),
            new("notifications", userAccounts.Notifications.ToString()),
            new("expiration", DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
        };
        return claims;
    }

    // Generate a JWT token for a given user model and JWT settings
    public static string GenTokenkey(UserDto model, JwtSettings jwtSettings)
    {
        try
        {
            if (model == null) throw new ArgumentException(null, nameof(model));

            // Get the secret key
            var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
            DateTime expireTime = DateTime.UtcNow.AddDays(1);

            // Create a JWT token with the specified claims and settings
            var JWToken = new JwtSecurityToken(
                issuer: jwtSettings.ValidIssuer,
                audience: jwtSettings.ValidAudience,
                claims: model.GetClaims(),
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(expireTime).DateTime,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(JWToken);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
