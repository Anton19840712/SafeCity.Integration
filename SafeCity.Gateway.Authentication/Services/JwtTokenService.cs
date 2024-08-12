using Microsoft.IdentityModel.Tokens;
using SafeCity.Gateway.Authentication.Models;
using SafeCity.Gateway.JwtConfiguration;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace SafeCity.Gateway.Authentication.Services
{
	/// <summary>
	/// Service generates web api token
	/// </summary>
	public class JwtTokenService : IJwtTokenService
	{
		private readonly List<User> _users = new()
		{
			// using roles in memory for simplicity:
			new("admin", "aDm1n", "Administrator", new[] { "processing112.read" }),
            new("user01", "u$3r01", "User", new[] { "processing112.noread" })
		};

		public AuthenticationToken GenerateAuthToken(LoginModel loginModel)
		{
			var user = _users.FirstOrDefault(u => u.Username == loginModel.Username && u.Password == loginModel.Password);

			if (user is null)
			{
				return null;
			}

			var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtExtensions.SecurityKey));
			var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
			var expirationTimeStamp = DateTime.Now.AddMinutes(5);

			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Name, user.Username),
				new Claim("Role", user.Role),
				new Claim("scope", string.Join(" ", user.Scopes))
			};

			var tokenOptions = new JwtSecurityToken(
				issuer: JwtExtensions.ValidIssuer,
				claims: claims,
				expires: expirationTimeStamp,
				signingCredentials: signingCredentials
			);

			var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

			return new AuthenticationToken(tokenString, (int)expirationTimeStamp.Subtract(DateTime.Now).TotalSeconds);
		}
	}
}
