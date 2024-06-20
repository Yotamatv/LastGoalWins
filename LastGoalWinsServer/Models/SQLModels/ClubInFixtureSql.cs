using LastGoalWinsServer.Models.General;
using LastGoalWinsServer.Models.MatchModel;
using Microsoft.IdentityModel.Tokens;
using System;

namespace LastGoalWinsServer.Models.SQLModels
{
    public class ClubInFixtureSql
    {
        public int id { get; set; }
        public int Fixtureid { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Logo { get; set; }
        public string Flag { get; set; }
        public bool Home { get; set; }
        public int? GoalsScored { get; set; }
        public DateTime LastUpdated { get; set; }

        public ClubInFixtureSql() { }
        public static ClubInFixtureSql CreateFromMatch(Match match, bool home)
        {
            var entity = new ClubInFixtureSql();

            if (home)
            {
                entity.id = match.Fixture.id * 10 + 1;
                entity.Name = match.Teams.Home.Name;
                entity.Logo = match.Teams.Home.Logo;
                entity.GoalsScored = match.Goals.Home.HasValue? match.Goals.Home:0;
            }
            else
            {
                entity.id = match.Fixture.id * 10;
                entity.Name = match.Teams.Away.Name;
                entity.Logo = match.Teams.Away.Logo;
                entity.GoalsScored = match.Goals.Away.HasValue ? match.Goals.Away : 0;
            }

            entity.Fixtureid = match.Fixture.id;
            entity.Country = match.League.Country;
            entity.Flag = !match.League.Flag.IsNullOrEmpty() ? match.League.Flag : entity.Logo;
            entity.Home = home;
            entity.LastUpdated = DateTime.Now;

            return entity;
        }

    }
}
