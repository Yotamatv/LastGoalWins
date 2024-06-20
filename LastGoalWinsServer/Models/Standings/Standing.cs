using LastGoalWinsServer.Models.General;

namespace LastGoalWinsServer.Models.Standings
{
    public class Standing
    {
        public int Rank { get; set; }
        public Team Team { get; set; }
        public int Points { get; set; }
        public int GoalsDiff { get; set; }
        public string Group { get; set; }
        public string Form { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public Record All { get; set; }
        public Record Home { get; set; }
        public Record Away { get; set; }
        public string Update { get; set; }
    }
}
