using LastGoalWinsServer.Models.SQLModels;

namespace LastGoalWinsServer.Models.General
{
    public class FixtureAndTeams
    {
        public FixtureSql Fixture { get; set; }
        public ClubInFixtureSql Home { get; set; }
        public ClubInFixtureSql Away { get; set; }
        public FixtureAndTeams(FixtureSql fixture, ClubInFixtureSql home, ClubInFixtureSql away)
        {
            Fixture = fixture;
            Home = home;
            Away = away;
        }
    }
}
