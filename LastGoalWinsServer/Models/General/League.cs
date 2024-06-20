using LastGoalWinsServer.Models.Standings;

namespace LastGoalWinsServer.Models.General
{
    public class League
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string? Logo { get; set; }
        public string Flag { get; set; }
        public int Season { get; set; }
        public string? Round { get; set; }
        public List<List<Standing>>? Standings { get; set; }
        public League(int id, string name, string country, string flag, int season, string? logo = null, string? round = null, List<List<Standing>>? standings = null)
        {
            Id = id;
            Name = name;
            Country = country;
            Logo = logo;
            Flag = flag;
            Season = season;
            Round = round;
            Standings = standings;
        }
    }
}
// epl 39
// ligue one 61
// serie a 135
// bundesliga 78
// la liga 140