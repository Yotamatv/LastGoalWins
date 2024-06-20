namespace LastGoalWinsServer.Models.Odds
{
    public class Bet
    {
        public int id { get; set; }
        public string Name { get; set; }
        public List<Values> Values { get; set; }
    }
}
