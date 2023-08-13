using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace SoccerClub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchApiController: ControllerBase
    {
        private readonly HttpClient _httpClient;

        public MatchApiController()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", "cae43e7da115410c80015e778078d5ab");
        }

        [HttpGet()]
        public async Task<IActionResult> GetStandingByClub(string club, string? year)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://api.football-data.org/v4/competitions/" + club + "/standings" + "?season=" + year);
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();

                return Ok(responseData);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }
        [HttpGet("matches/{id}")]
        public async Task<IActionResult> GetMatchesForClub(string id)
        {
            //X - Unfold - Goals: true
            _httpClient.DefaultRequestHeaders.Add("X-Unfold-Goals", "false");

            HttpResponseMessage response = await _httpClient.GetAsync("https://api.football-data.org/v4/teams/" + id + "/matches");
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();

                return Ok(responseData);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetMatchesDetail(string id)
        {

            HttpResponseMessage response = await _httpClient.GetAsync("http://api.football-data.org/v4/matches/" + id);
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();

                return Ok(responseData);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }
    }
}
