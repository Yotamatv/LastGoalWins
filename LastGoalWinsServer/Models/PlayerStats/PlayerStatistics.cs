using LastGoalWinsServer.Models.General;
using LastGoalWinsServer.Models.MatchModel;

namespace LastGoalWinsServer.Models.PlayerStats
{
    public class PlayerStatistics
    {
        public Team? Team { get; set; }
        public League? League { get; set; }
        public Games? Games { get; set; }
        public Goals? Goals { get; set; }
    //    public Substitutes? Substitutes { get; set; }
    //    public Shots? Shots { get; set; }
    //    public Passes? Passes { get; set; }
    //    public Tackles? Tackles { get; set; }
    //    public Duels? Duels { get; set; }
    //    public Dribbles? Dribbles { get; set; }
    //    public Fouls? Fouls { get; set; }
    //    public Cards? Cards { get; set; }
    //    public Penalty? Penalty { get; set; }
    }
}
