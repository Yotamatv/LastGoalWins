using LastGoalWinsServer.Models.Events;

namespace LastGoalWinsServer.Models.SQLModels
{
    public class EventSql
    {
        public int Id { get; set; }
        public int ClubsInFixturesId { get; set; }
        public string? Player1 { get; set; }
        public string? Player2 { get; set; }
        public string? Type { get; set; }
        public string? Details { get; set; }
        public int TimeElapsed { get; set; }
        public EventSql() { }
        public EventSql(EventModel eventModel, int clubInFixture) 
        {
            ClubsInFixturesId = clubInFixture;
            Player1 = eventModel.Player.Name;
            Player2 = eventModel.Assist.Name;
            Type = eventModel.Type;
            Details = eventModel.Detail;
            TimeElapsed=eventModel.Time.Elapsed;
        }

    }
}
