using SafeCity.Gateway.Authentication.Models;

namespace SafeCity.Gateway.Authentication.Services
{
	public interface IJwtTokenService
	{
		AuthenticationToken? GenerateAuthToken(LoginModel loginModel);
	}
}