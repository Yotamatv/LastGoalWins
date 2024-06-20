using LastGoalWinsServer.Auth;
using LastGoalWinsServer.Middleware;
using LastGoalWinsServer.Services;
using LastGoalWinsServer.Services.DataBase;
using LastGoalWinsServer.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LastGoalWinsServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            var builder = WebApplication.CreateBuilder(args);

            // Configure services
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000", "https://localhost:3000")
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
            });

            // Register DbContext with SQL Server configuration
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {

                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateLifetime = true,
                                    ValidateIssuerSigningKey = true,
                                    ValidIssuer = "FootballServer",
                                    ValidAudience = "FootballFront",
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtHelper.secretKey))
                                };
                            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("MustBeAdmin", policy => policy.RequireClaim("type", "Admin"));
            });

            // Register the PasswordHasher as a singleton
            builder.Services.AddSingleton<IPasswordHasher<object>, PasswordHasher<object>>();

            // Register other services
            builder.Services.AddScoped<MatchesApiService>();
            builder.Services.AddScoped<MatchesDbService>();
            builder.Services.AddScoped<UsersService>();
            builder.Services.AddScoped<UsersDbService>();
            builder.Services.AddScoped<PasswordHelper>();

            // Register HttpClient with rapidapi configuration
            builder.Services.AddHttpClient("rapidapi", client =>
            {
                client.BaseAddress = new Uri("https://api-football-v1.p.rapidapi.com");
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "da403f047amsh8464e838848db8dp17dc49jsn029ad3a02f7d");
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "api-football-v1.p.rapidapi.com");
            });

            // Register controllers and Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Football Bets API", Version = "v1" });
            });

            var app = builder.Build();

            // Use CORS policy
            app.UseCors(MyAllowSpecificOrigins);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Football Bets API v1");
                });
            }

            // Register the custom JWT logging middleware before Authentication and Authorization
            app.UseMiddleware<JwtLoggingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
