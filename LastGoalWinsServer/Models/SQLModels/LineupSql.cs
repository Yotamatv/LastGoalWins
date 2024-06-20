using LastGoalWinsServer.Models.General;
using LastGoalWinsServer.Models.Lineups;
using Microsoft.IdentityModel.Tokens;

namespace LastGoalWinsServer.Models.SQLModels
{
    public class LineupSql
    {
        public int Id { get; set; }
        public int ClubsInFixturesId { get; set; }
        public string? Name { get; set; }
        public int? Number { get; set; }
        public int? PositionX { get; set; }
        public int? PositionY { get; set; }
        // Parameterless constructor required by EF Core
        public LineupSql() { }

        // Custom constructor for manual instantiation
        public LineupSql(Player player, int clubsInFixturesId)
        {
            ClubsInFixturesId = clubsInFixturesId;
            Name = player.Name;
            Number = player.Number;
            PositionX = player.Grid.IsNullOrEmpty() ? (int?)null : player.Grid[0] - 48;//-48 to convert from ASCII to number
            PositionY = player.Grid.IsNullOrEmpty() ? (int?)null : player.Grid[2] - 48;
        }
    }

}
