using Microsoft.AspNetCore.Mvc;

namespace SafeCity.Gateway.Government.Controllers
{
	[Route("api/governments")]
	[ApiController]
	public class GovernmentController : ControllerBase
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<string>>> Get()
		{
			await Task.Delay(100);
			return new string[] { "Dhananjay", "Nidhish", "Vijay", "Nazim", "Alpesh" };
		}
	}
}
