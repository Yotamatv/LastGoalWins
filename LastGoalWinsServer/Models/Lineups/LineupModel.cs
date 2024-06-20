using LastGoalWinsServer.Models.General;

namespace LastGoalWinsServer.Models.Lineups
{
    public class LineupModel
    {
        public Team Team { get; set; }
        public string Formation { get; set; }
        public List<PlayerInLineup> StartXI { get; set; }
        public List<PlayerInLineup> Substitutes { get; set; }
    }
}
