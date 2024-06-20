using LastGoalWinsServer.Models.Lineups;

namespace LastGoalWinsServer.Models.General
{
    public class Team
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public bool? Winner { get; set; }
        public Colors Colors { get; set; }
    }
}
