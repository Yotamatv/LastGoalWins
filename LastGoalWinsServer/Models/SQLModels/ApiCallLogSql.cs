namespace LastGoalWinsServer.Models.SQLModels
{
    public class ApiCallLogSql
    {
        public int Id { get; set; }
        public int? Fixtureid { get; set; }
        public int? Leagueid { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? Received { get; set; }
        public string Method { get; set; }
    }
}
