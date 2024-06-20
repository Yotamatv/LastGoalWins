
namespace LastGoalWinsServer.Models.Standings
{
    public class Record
    {
        public int Played { get; set; }
        public int Win { get; set; }
        public int Draw { get; set; }
        public int Lose { get; set; }
        public GoalsStandings Goals { get; set; }
    }
}
