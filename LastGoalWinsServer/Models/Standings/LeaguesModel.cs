using LastGoalWinsServer.Models.General;

namespace LastGoalWinsServer.Models.Standings
{
    public class LeaguesModel
    {
        public League League { get; set; }
        public LeaguesModel(League league) 
        {
            League = league;
        }
    }
}
