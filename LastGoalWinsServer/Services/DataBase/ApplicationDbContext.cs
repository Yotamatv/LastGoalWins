using LastGoalWinsServer.Models.SQLModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace LastGoalWinsServer.Services.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<BetsSql> Bets { get; set; }
        public DbSet<ClubInFixtureSql> ClubsInFixtures { get; set; }
        public DbSet<EventSql> Events { get; set; }
        public DbSet<FixtureSql> Fixtures { get; set; }
        public DbSet<LeagueSql> Leagues { get; set; }
        public DbSet<LeagueStandingSql> LeagueStandings { get; set; }
        public DbSet<LineupSql> Lineups { get; set; }
        public DbSet<PlayerLeaderboardSql> PlayerLeaderboards { get; set; }
        public DbSet<UserSql> Users { get; set; }
        public DbSet<ApiCallLogSql> ApiCallLogs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure LeagueStandingSql entity
            modelBuilder.Entity<LeagueStandingSql>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.Leagueid).IsRequired();
                entity.Property(e => e.Rank_).IsRequired();
                entity.Property(e => e.Points).IsRequired();
                entity.Property(e => e.Club).IsRequired();
                entity.Property(e => e.ClubLogo).IsRequired();
                entity.Property(e => e.GamesPlayed).IsRequired();
                entity.Property(e => e.GoalsDiff).IsRequired();
                entity.Property(e => e.GoalsFor).IsRequired();
                entity.Property(e => e.GoalsAgainst).IsRequired();
                entity.Property(e => e.Won).IsRequired();
                entity.Property(e => e.Lost).IsRequired();
                entity.Property(e => e.Drawn).IsRequired();
                entity.Property(e => e.Form).IsRequired();
                entity.Property(e => e.LastUpdated).IsRequired();
            });
            modelBuilder.Entity<FixtureSql>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Leagueid).IsRequired();
                entity.Property(e => e.Referee).HasMaxLength(255);
                entity.Property(e => e.Timestamp).IsRequired();
                entity.Property(e => e.Timezone).HasMaxLength(255);
                entity.Property(e => e.FixtureDate).IsRequired();
                entity.Property(e => e.FixtureRound);
                entity.Property(e => e.Season).IsRequired();
                entity.Property(e => e.Venue).HasMaxLength(255);
                entity.Property(e => e.Status).HasMaxLength(255);
                entity.Property(e => e.HomeWinOdd).IsRequired();
                entity.Property(e => e.AwayWinOdd).IsRequired();
                entity.Property(e => e.DrawOdd).IsRequired();
                entity.Property(e => e.LastUpdated).IsRequired();
                entity.Property(e => e.StatusType).IsRequired().HasMaxLength(20);
            });

            modelBuilder.Entity<ClubInFixtureSql>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.Fixtureid).IsRequired();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Country).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Logo).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Flag).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Home).IsRequired();
                entity.Property(e => e.GoalsScored);
                entity.Property(e => e.LastUpdated).IsRequired();
            });
            modelBuilder.Entity<LineupSql>(entity =>
            {
                entity.HasKey(e => e.Id); // Configure the primary key
                entity.Property(e => e.ClubsInFixturesId).IsRequired(); // Configure ClubsInFixturesId as required
                entity.Property(e => e.Name).HasMaxLength(255); // Configure Name with a max length of 255
                entity.Property(e => e.Number); // Configure Number as optional
                entity.Property(e => e.PositionX); // Configure PositionX as optional
                entity.Property(e => e.PositionY); // Configure PositionY as optional
            });

        }
    }
}
