using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ECommerceAPI.Models;
using Microsoft.IdentityModel.Tokens;

public class TokenService : ITokenService
{
    private IConfiguration Configuration { get; }

    public TokenService(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var serverSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:ServerSecret"]));
        var now = DateTime.UtcNow;
        var issuer = Configuration["JWT:Issuer"];
        var audience = Configuration["JWT:Audience"];
        var identity = new ClaimsIdentity(new Claim[]
            {
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Role, user.Role),
                            new Claim(ClaimTypes.Name, user.Id.ToString())
            });
        var signingCredentials = new SigningCredentials(serverSecret, SecurityAlgorithms.HmacSha256);
        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateJwtSecurityToken(issuer, audience, identity,
        now, now.Add(TimeSpan.FromHours(1)), now, signingCredentials);
        var encodedJwt = handler.WriteToken(token);
        return encodedJwt;
    }
    
}