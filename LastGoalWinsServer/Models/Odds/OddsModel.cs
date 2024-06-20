using LastGoalWinsServer.Models.General;

namespace LastGoalWinsServer.Models.Odds
{
    public class OddsModel
    {
        public League League { get; set; }
        public Fixture Fixture { get; set; }
        public string Update { get; set; }
        public List<Bookmaker> Bookmakers { get; set; }
    }
}
