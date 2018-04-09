﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using funda.common.logging;
using funda.model;
using funda.repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
	[Produces("application/json")]
	[Route("api/posts")]
    public class PostsController : Controller
    {
		private readonly IAsyncRepository<Post> _repos;
		private readonly IFundaLogger<Post> _logger;

		public PostsController(IAsyncRepository<Post> repos, IFundaLogger<Post> logger)
		{
			_repos = repos;
			_logger = logger;
			_repos.Initialize();
		}

        [HttpGet("{id}")]
        public async Task<IActionResult> ReadAsync(int id)
		{
			try
			{
				var result = await _repos.ReadAsync(id);
				if (result.Payload != null)
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