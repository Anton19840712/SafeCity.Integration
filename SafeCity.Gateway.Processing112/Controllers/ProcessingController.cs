using Microsoft.AspNetCore.Mvc;
using SafeCity.Gateway.Business.Models.Request;

namespace SafeCity.Gateway.Processing112.Controllers
{
	[Route("api/processing112")]
	[ApiController]
	public class ProcessingController : ControllerBase
	{
		static List<Card112ChangedRequest> card112ChangedRequests = new List<Card112ChangedRequest>(){};

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Card112ChangedRequest>>> Get()
		{
			var products = await GetProductsAsync();
			await Task.Delay(500);
			return Ok(products);
		}

		[HttpPost]
		public async Task<ActionResult<Card112ChangedRequest>> Post(Card112ChangedRequest card112ChangedRequest)
		{
			card112ChangedRequests.Add(card112ChangedRequest);
			await Task.Delay(500);
			return CreatedAtAction(nameof(Get), new { id = 24 }, card112ChangedRequest);
		}

		private Task<List<Card112ChangedRequest>> GetProductsAsync()
		{
			return Task.FromResult(card112ChangedRequests);
		}
	}
}
