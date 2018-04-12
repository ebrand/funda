﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using funda.common.logging;
using funda.model;
using funda.repository;
using Microsoft.AspNetCore.Mvc;

namespace api.controllers
{
	//[BasicAuthentication]
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
		}

		[HttpGet]
		[Route("api/posts")]
		public async Task<IActionResult> ReadAllAsync()
		{
			try
			{
				var result = await _repos.ReadAllAsync();

				if (result.Payload != null)
					return Ok(result.Payload);
				else
					return NoContent();
			}
			catch (Exception exc)
			{
				_logger.LogError(Events.ApiController.Read.Failure, exc.Message, exc);
				return BadRequest(exc);
			}
		}

		/// <summary>
		/// This will action respond to two different routes:
		///		1: http://xxx/api/posts/100		 <- returns a single post with ID: 100
		///		2: http://xxx/api/posts?q=potato <- returns search results on keyword: 'potato'
		/// </summary>
		/// <param name="id">The identifier of the post to return.</param>
		/// <param name="q">The search keyword to use in searching posts.</param>
		/// <returns>A list of <see cref="Post"></see>s.</returns>
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

		[HttpGet]
		[Route("api/posts/latest")]
		public async Task<IActionResult> ReadLatest()
		{
			try
			{
				var result = await Task.Run(() => new AsyncResponse<Post>(new List<Post>() { Post.SamplePost }, AsyncResponseType.Success, 0));

				if (result.Payload != null)
					return Ok(result.Payload);
				else
					return NoContent();
			}
			catch (Exception exc)
			{
				_logger.LogError(Events.ApiController.Read.Failure, exc.Message, exc);
				return BadRequest(exc);
			}
		}
    }
}