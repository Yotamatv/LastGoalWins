using LastGoalWinsServer.Models.MatchModel;
using System.Text.RegularExpressions;

namespace LastGoalWinsServer.Models.SQLModels
{
    public class FixtureSql
    {
        public int Id { get; set; }
        public int Leagueid { get; set; }
        public string? Referee { get; set; }
        public long Timestamp { get; set; }
        public string? Timezone { get; set; }
        public DateTime FixtureDate { get; set; }
        public int? FixtureRound { get; set; }
        public int Season { get; set; }
        public string? Venue { get; set; }
        public string? Status { get; set; }
        public double HomeWinOdd { get; set; }
        public double AwayWinOdd { get; set; }
        public double DrawOdd { get; set; }
        public DateTime LastUpdated { get; set; }
        public string StatusType { get; set; }
        public FixtureSql() { }
        public FixtureSql(MatchModel.Match match) 
        {
            Id=match.Fixture.id;
            Leagueid=match.League.Id;
            Referee = match.Fixture.Referee;
            Timestamp = match.Fixture.Timestamp;
            Timezone = match.Fixture.Timezone;
            FixtureDate = match.Fixture.Date;
            FixtureRound =ExtractRoundNumber(match.League.Round);
            Season = match.League.Season;
            Venue = match.Fixture.Venue?.Name;
            Status = match.Fixture.Status?.Short;
            HomeWinOdd = 0;
            AwayWinOdd = 0;
            DrawOdd = 0;
            LastUpdated = DateTime.Now;
            StatusType = GetStatusType(match.Fixture.Status.Short);
        }
        private static readonly Dictionary<string, string> StatusMapping = new Dictionary<string, string>
    {
        {"TBD", "Scheduled"},
        {"NS", "Scheduled"},
        {"1H", "In Play"},
        {"HT", "In Play"},
        {"2H", "In Play"},
        {"ET", "In Play"},
        {"BT", "In Play"},
        {"P", "In Play"},
        {"SUSP", "In Play"},
        {"INT", "In Play"},
        {"FT", "Finished"},
        {"AET", "Finished"},
        {"PEN", "Finished"},
        {"PST", "Postponed"},
        {"CANC", "Cancelled"},
        {"ABD", "Abandoned"},
        {"AWD", "Not Played"},
        {"WO", "Not Played"},
        {"LIVE", "In Play"}
    };
        public string GetStatusType(string shortStatus)
        {
            if (StatusMapping.TryGetValue(shortStatus, out string type))
            {
                return type;
            }
            return "Unknown";
        }
        public static int ExtractRoundNumber(string input)
        {
            // Regular expression to match one or more digits
            Regex regex = new Regex(@"\d+");
            System.Text.RegularExpressions.Match match = regex.Match(input);
            if (match.Success)
            {
                return int.Parse(match.Value);
            }
            Console.WriteLine("No number found in the input string");
            return 0;
        }
    }
}
