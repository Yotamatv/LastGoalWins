using LastGoalWinsServer.Models.Standings;
using System;

namespace LastGoalWinsServer.Models.SQLModels
{
    public class LeagueStandingSql
    {
        public int id { get; set; }
        public int Leagueid { get; set; }
        public int Rank_ { get; set; }
        public int Points { get; set; }
        public string Club { get; set; }
        public string ClubLogo { get; set; }
        public int GamesPlayed { get; set; }
        public int GoalsDiff { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int Won { get; set; }
        public int Lost { get; set; }
        public int Drawn { get; set; }
        public string Form { get; set; }
        public DateTime LastUpdated { get; set; }

        // Parameterless constructor
        public LeagueStandingSql() { }

        // Additional constructor for convenience
        public LeagueStandingSql(int leagueid, Standing leagueStanding)
        {
            Leagueid = leagueid;
            Rank_ = leagueStanding.Rank;
            Points = leagueStanding.Points;
            Club = leagueStanding.Team.Name;
            ClubLogo = leagueStanding.Team.Logo;
            GamesPlayed = leagueStanding.All.Played;
            GoalsFor = leagueStanding.All.Goals.For;
            GoalsAgainst = leagueStanding.All.Goals.Against;
            GoalsDiff = GoalsFor - GoalsAgainst;
            Won = leagueStanding.All.Win;
            Lost = leagueStanding.All.Lose;
            Drawn = leagueStanding.All.Draw;
            Form = leagueStanding.Form;
            LastUpdated = DateTime.Now;
        }
    }
}
