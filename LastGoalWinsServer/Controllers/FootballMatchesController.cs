using LastGoalWinsServer.Models.MatchModel;
using LastGoalWinsServer.Services;
using LastGoalWinsServer.Services.DataBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LastGoalWinsServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FootballMatchesController : ControllerBase
    {
        private readonly MatchesApiService _matchesApiService;
        private readonly MatchesDbService _matchesDbService;

        public FootballMatchesController(MatchesApiService matchesApiService, MatchesDbService matchesDbService)
        {
            _matchesApiService = matchesApiService;
            _matchesDbService = matchesDbService;
        }

        [HttpGet("Fixtures/{leagueid}/{startDate?}/{endDate?}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllMatchesByLeague(int leagueid, string startDate = null, string endDate = null)
        {
            DateTime start;
            DateTime end;

            if (string.IsNullOrEmpty(startDate))
            {
                start = DateTime.Now;
            }
            else
            {
                start = DateTime.Parse(startDate);
            }

            if (string.IsNullOrEmpty(endDate))
            {
                end = start.AddDays(7);
            }
            else
            {
                end = DateTime.Parse(endDate);
            }

            var matchesResponse = await _matchesDbService.GetFixturesByLeague(leagueid, start, end);
            return Ok(matchesResponse);
        }


        [HttpGet("test")]
        [AllowAnonymous]
        public IActionResult GetAllMatchesTest()
        {
            Console.WriteLine("GetAllMatchesTest");
            return Ok(MockResponse.mockresponse);
        }

        [HttpGet("LeagueTable/{leagueid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLeagueTable(int leagueid)
        {
            var leagueTableResponse = await _matchesDbService.GetLeagueTable(leagueid);
            return Ok(leagueTableResponse);
        }

        [HttpGet("TopScorers/{leagueid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTopScorers(int leagueid)
        {
            var topScorersResponse = await _matchesApiService.GetTopScorers(leagueid);
            return Ok(topScorersResponse);
        }

        [HttpGet("Odds/{leagueid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetOdds(int leagueid)
        {
            var oddsResponse = await _matchesApiService.GetOdds(leagueid);
            return Ok(oddsResponse);
        }

        [HttpGet("Fixture/{fixtureid}/Lineups")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLineups(int fixtureid)
        {
            var lineupsResponse = await _matchesDbService.GetLineup(fixtureid);
            return Ok(lineupsResponse);
        }

        [HttpGet("Fixture/{fixtureid}/Events")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEvents(int fixtureid)
        {
            var eventsResponse = await _matchesDbService.GetEvents(fixtureid);
            return Ok(eventsResponse);
        }
    }
}

//https://rapidapi.com/api-sports/api/api-football/