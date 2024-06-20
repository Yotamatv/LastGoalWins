namespace LastGoalWinsServer.Models.SQLModels
{
    public class PlayerLeaderboardSql
    {
        public int id { get; set; }
        public int Leagueid { get; set; }
        public string Name { get; set; }
        public string Club { get; set; }
        public string ClubLogo { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
