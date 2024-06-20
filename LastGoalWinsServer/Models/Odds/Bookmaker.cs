namespace LastGoalWinsServer.Models.Odds
{
    public class Bookmaker
    {
        public int id { get; set; }
        public string Name { get; set; }
        public List<Bet> Bets { get; set; }
    }
}
