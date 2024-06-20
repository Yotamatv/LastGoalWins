using LastGoalWinsServer.Models.General;

namespace LastGoalWinsServer.Models.PlayerStats
{
    public class PlayerStatsModel
    {
        public Player? Player { get; set; }
        public List<PlayerStatistics>? Statistics { get; set; }

    }
}
