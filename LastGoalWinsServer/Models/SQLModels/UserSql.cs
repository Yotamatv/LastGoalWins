namespace LastGoalWinsServer.Models.SQLModels
{
    public class UserSql
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public double Balance { get; set; }
        public string ProfilePictureUrl { get; set; }
    }
}
