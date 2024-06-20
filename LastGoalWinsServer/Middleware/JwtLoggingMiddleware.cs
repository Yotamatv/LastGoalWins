namespace LastGoalWinsServer.Middleware
{
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public class JwtLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                var token = context.Request.Headers["Authorization"].ToString();
                if (token.StartsWith("Bearer ", System.StringComparison.OrdinalIgnoreCase))
                {
                    var jwt = token.Substring("Bearer ".Length).Trim();
                    // Log the JWT to the console
                    System.Console.WriteLine($"JWT: {jwt}");
                }
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }


}
