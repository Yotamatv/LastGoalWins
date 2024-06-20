namespace LastGoalWinsServer.Exceptions
{
    public class AuthenticationException: Exception
    {
        public AuthenticationException() :base("Email or Password are wrong") { }
    }
}
