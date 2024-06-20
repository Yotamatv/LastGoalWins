using LastGoalWinsServer.Models.Events;
using LastGoalWinsServer.Models.General;
using LastGoalWinsServer.Models.Lineups;
using LastGoalWinsServer.Models.MatchModel;
using LastGoalWinsServer.Models.Odds;
using LastGoalWinsServer.Models.PlayerStats;
using LastGoalWinsServer.Models.Standings;
using LastGoalWinsServer.Services.DataBase;
using System.Diagnostics.Metrics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace LastGoalWinsServer.Services
{

    public class MatchesApiService
    {
        List<int> leagues = new List<int>
        {
            4,    //Euro
            39,   // EPL
            61,    // Ligue One
            135,  // Serie A
            78,   // Bundesliga
            140,  // La Liga
        };

        private readonly IHttpClientFactory _httpClientFactory;
        private DateOnly date = new DateOnly();
        private int currentSeason = new int();
        private string baseUri = "https://api-football-v1.p.rapidapi.com/v3/";
        public MatchesApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            date = DateOnly.FromDateTime(DateTime.Now);
            currentSeason = date.Month >= 8 ? date.Year : date.Year - 1;
        }

        string formattedDate(DateTime date)
        {
            string year = date.Year.ToString();
            string month = date.Month.ToString().PadLeft(2, '0');
            string day = date.Day.ToString().PadLeft(2, '0');
            return $"{date.Year}-{month}-{day}";
        }
        public async Task<ResponseModel<Match>> GetAllMatchesByLeague(int leagueid, DateTime startDate, DateTime endDate)
        {
            if(leagueid==4)
            {
                currentSeason++;
            }
            string RequestUri = $"{baseUri}fixtures?league={leagueid}&season={startDate}&from={formattedDate(startDate)}&to={formattedDate(endDate)}";
            var ParsedResponse = await new ApiService<Match>(_httpClientFactory).UseAPI(RequestUri);//Makes API request and returns it as a selected class
            Console.WriteLine("Length: " + ParsedResponse.Response.Count);
            return ParsedResponse;
        }

        public async Task<List<ResponseModel<Match>>> GetAllMatches(DateTime startDate, DateTime endDate)
        {
            var allMatches = new List<ResponseModel<Match>>();
            foreach (int league in leagues)
            {
                allMatches.Add(await GetAllMatchesByLeague(league, startDate, endDate));
            }
            return allMatches;
        }
        public async Task<ResponseModel<LeaguesModel>> GetLeagueTable(int leagueid)
        {
            if (leagueid == 4)
            {
                currentSeason++;
            }
            string RequestUri = $"{baseUri}standings?season={currentSeason}&league={leagueid}";
            var ParsedResponse = await new ApiService<LeaguesModel>(_httpClientFactory).UseAPI(RequestUri);// Makes API request and returns it as a selected class
            return ParsedResponse;
        }
        public async Task<List<ResponseModel<PlayerStatsModel>>> GetTopScorers(int leagueid)
        {
            if (leagueid == 4)
            {
                currentSeason++;
            }
            List<ResponseModel<PlayerStatsModel>> GoalsAndAssists = new List<ResponseModel<PlayerStatsModel>>();
            string ScorersRequestUri = $"{baseUri}players/topscorers?season={currentSeason}&league={leagueid}";
            string AssistsRequestUri = $"{baseUri}players/topAssists?season={currentSeason}&league={leagueid}";
            GoalsAndAssists.Add(await new ApiService<PlayerStatsModel>(_httpClientFactory).UseAPI(ScorersRequestUri));//Makes API request and returns it as a selected class
            GoalsAndAssists.Add(await new ApiService<PlayerStatsModel>(_httpClientFactory).UseAPI(AssistsRequestUri));
            return GoalsAndAssists;
        }
        public async Task<ResponseModel<OddsModel>> GetOdds(int leagueid)
        {
            string RequestUri = $"{baseUri}odds?season={currentSeason}&league={leagueid}&bookmaker=8&bet=1";
            var ParsedResponse = await new ApiService<OddsModel>(_httpClientFactory).UseAPI(RequestUri);//Makes API request and returns it as a selected class
            return ParsedResponse;
        }
        public async Task<ResponseModel<LineupModel>> GetLineups(int Fixtureid)
        {
            string RequestUri = $"{baseUri}fixtures/lineups?fixture={Fixtureid}";
            var ParsedResponse = await new ApiService<LineupModel>(_httpClientFactory).UseAPI(RequestUri);//Makes API request and returns it as a selected class
            return ParsedResponse;
        }
        public async Task<ResponseModel<EventModel>> GetEvents(int Fixtureid)
        {
            string RequestUri = $"{baseUri}fixtures/events?fixture={Fixtureid}";
            var ParsedResponse = await new ApiService<EventModel>(_httpClientFactory).UseAPI(RequestUri);//Makes API request and returns it as a selected class
            return ParsedResponse;
        }
    }

}
