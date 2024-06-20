using LastGoalWinsServer.Models.PlayerStats;
using LastGoalWinsServer.Models.Standings;
using Newtonsoft.Json.Linq;
using System;

namespace LastGoalWinsServer.Models.SQLModels
{
    public class PlayerLeaderboardSql
    {
        public int id { get; set; }
        public int Leagueid { get; set; }
        public string Name { get; set; }
        public string Club { get; set; }
        public string ClubLogo { get; set; }
        public int? Goals { get; set; }
        public int? Assists { get; set; }
        public DateTime LastUpdated { get; set; }
        public PlayerLeaderboardSql() { }
        public PlayerLeaderboardSql(int leagueid, PlayerStatsModel playerStatsModel)
        {
            Leagueid = leagueid;
            Name = playerStatsModel.Player.Name;
            Club = playerStatsModel.Statistics[0].Team.Name;
            ClubLogo = playerStatsModel.Statistics[0].Team.Logo;
            Goals = playerStatsModel.Statistics[0].Goals.Total.HasValue ?
                playerStatsModel.Statistics[0].Goals.Total : 0;
            Assists = playerStatsModel.Statistics[0].Goals.Assists.HasValue ?
                playerStatsModel.Statistics[0].Goals.Assists : 0;
            LastUpdated = DateTime.Now;
        }
    }
}
