namespace LastGoalWinsServer.Models.General
{
    public class Paging
    {
        public int Current { get; set; }
        public int Total { get; set; }
        public Paging(int current, int total)
        {
            Current = current;
            Total = total;
        }
    }
}
