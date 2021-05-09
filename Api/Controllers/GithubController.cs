using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GithubController : ControllerBase
    {
        private readonly HttpClient _client;
        private readonly ILogger<Github> _logger;
        private const string UrlOrganization = "https://api.github.com/orgs/takenet/repos";

        public GithubController(HttpClient client, ILogger<Github> logger)
        {
            _client = client;
            _logger = logger;
        }

        /// <summary>
        /// Take the oldest N reposotios from TakeBlip
        /// </summary>
        /// <param name="numberOfRepositories">Number of repositories to be returned</param>
        /// <returns>A json object containing repository information</returns>
        [HttpGet("{numberOfRepositories:int=3}")]
        public async Task<IActionResult> Get(int numberOfRepositories)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            _client.DefaultRequestHeaders.Add("User-Agent", "Chatbot Challenge");
            try
            {
                var responseStream = await _client.GetStreamAsync(UrlOrganization);
                var repositories = await JsonSerializer.DeserializeAsync<List<Github>>(responseStream);
                var ordered = repositories!.OrderBy(repo =>
                    repo.RepositoryCreatedDatetime);
                return Ok(ordered.Take(numberOfRepositories));
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Could not connect to the github API");
                return Problem("Could not connect to the github API");
            }
        }
    }
}