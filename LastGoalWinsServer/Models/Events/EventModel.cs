using LastGoalWinsServer.Models.General;

namespace LastGoalWinsServer.Models.Events
{
    public class EventModel
    {
        public Time Time { get; set; }
        public Team Team { get; set; }
        public Player Player { get; set; }
        public Player? Assist { get; set; }
        public string Type { get; set; }
        public string Detail { get; set; }
        public string? Comments { get; set; }
    }
}
