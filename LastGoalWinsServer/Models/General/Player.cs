using LastGoalWinsServer.Models.PlayerStats;

namespace LastGoalWinsServer.Models.General
{
    public class Player
    {
        public int? id { get; set; }
        public string? Name { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public int? Age { get; set; }
        public Birth? Birth { get; set; }
        public string? Nationality { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public bool? Injured { get; set; }
        public string? Photo { get; set; }
        public string? Pos { get; set; }
        public string? Grid { get; set; }
        public int? Number { get; set; }
    }
}
