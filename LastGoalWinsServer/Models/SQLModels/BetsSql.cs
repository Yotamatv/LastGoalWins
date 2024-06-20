using System.ComponentModel.DataAnnotations;

namespace LastGoalWinsServer.Models.SQLModels
{
    public class BetsSql
    {
        public int id { get; set; }
        public int Userid { get; set; }
        public int Fixtureid { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
    }
}
