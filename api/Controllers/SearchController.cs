using System;
using Microsoft.AspNetCore.Mvc;
using funda.repository;
using funda.model;
using System.Threading.Tasks;
using funda.common.logging;

namespace funda.api.Controllers
{
	[Produces("application/json")]
	//[Route("api/search")]
	public class SearchController : Controller
	{
		private readonly IAsyncRepository<Post> _repository;
		private readonly IFundaLogger<SearchController> _logger;

		public SearchController(IAsyncRepository<Post> repository, IFundaLogger<SearchController> logger)
		{
			_repository = repository;
			_logger = logger;
		}
		/// <summary>
		/// This action responds to the route: http://xxx/api/search/potato
		/// by returning search results on keyword: 'potato'.
		/// </summary>
		/// <param name="q">The search term for which to search.</param>
		/// <returns>A list of <see cref="Post"/>s.</returns>
		[HttpGet("{q}")]
		//[Route("api/search/{q}")]
		public async Task<IActionResult> PostsKeywordSearchAsync(string q)
		{
			try
			{
				var result = await _repository.KeywordSearchAsync(q);

				if(result.Payload != null)
					return Ok(result.Payload);
				else
					return NoContent();
			}
			catch(Exception exc)
			{
				_logger.LogError(Events.ApiController.Read.Failure, exc.Message, exc);
				return BadRequest(exc);
			}
		}
	}
}