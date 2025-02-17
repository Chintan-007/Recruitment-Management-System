using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration config;
    private readonly SymmetricSecurityKey key;

    private readonly UserManager<Users> _userManager;

    public TokenService(IConfiguration config,UserManager<Users> userManager){
        this.config = config;
        this.key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Signingkey"]));
        _userManager = userManager;
    }
    public async Task<string> CreateToken(Users user)
    {
        // Retrieve the roles for the user
        var roles = await _userManager.GetRolesAsync(user);


        var claims = new List<Claim>{
            new Claim(JwtRegisteredClaimNames.Email,user.Email),
            new Claim(JwtRegisteredClaimNames.GivenName,user.UserName),
            new Claim("id",user.Id),
        };

        // Add roles as claims
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));  // Add each role to the claims
        }


        var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
        
        var tokenDescriptor = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds,
            Issuer = config["JWT:Issuer"],
            Audience = config["JWT:Audience"],
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}