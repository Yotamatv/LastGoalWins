namespace LastGoalWinsServer.Models.General
{
    public class Parameters
    {
        public string? League { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? Bet { get; set; }
        public string? Bookmaker { get; set; }
        public string? Season { get; set; }
        public string? Fixture { get; set; }

        public Parameters(string league, string from, string to, string season, string bet, string bookmaker)
        {
            League = league;
            From = from;
            To = to;
            Season = season;
            Bet = bet;
            Bookmaker = bookmaker;
        }
    }
}
