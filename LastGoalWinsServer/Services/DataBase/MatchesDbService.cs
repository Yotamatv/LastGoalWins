using LastGoalWinsServer.Models.Events;
using LastGoalWinsServer.Models.General;
using LastGoalWinsServer.Models.Lineups;
using LastGoalWinsServer.Models.MatchModel;
using LastGoalWinsServer.Models.SQLModels;
using LastGoalWinsServer.Models.Standings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
using Match = LastGoalWinsServer.Models.MatchModel.Match;

namespace LastGoalWinsServer.Services.DataBase
{
    public class MatchesDbService
    {
        private readonly ApplicationDbContext _context;
        private readonly MatchesApiService _matchesApiService;
        public MatchesDbService(ApplicationDbContext context, MatchesApiService matchesApiService)
        {
            _context = context;
            _matchesApiService = matchesApiService;
        }
        public async Task<bool> UpdateLeagueTable(ResponseModel<LeaguesModel> leaguesModel)
        {
            int leagueid = leaguesModel.Response[0].League.Id;
            var leagueStandings = new List<Standing>();
            foreach (var leagueStanding in leaguesModel.Response[0].League.Standings[0])
            {
                //Map a list of all the standings because it's a list in list
                leagueStandings.Add(leagueStanding);
            }
            foreach (var leagueStanding in leagueStandings)
            {
                try
                {
                    var standingSql = new LeagueStandingSql(leagueid, leagueStanding);
                    var existingStanding = _context.LeagueStandings.Where(standing => standing.Leagueid == leagueid
                    && standing.Rank_ == standingSql.Rank_).FirstOrDefault();
                    if (existingStanding == null)
                    {
                        _context.LeagueStandings.Add(standingSql);
                    }
                    else
                    {
                        existingStanding.Leagueid = leagueid;
                        existingStanding.Rank_ = standingSql.Rank_;
                        existingStanding.Points = standingSql.Points;
                        existingStanding.Club = standingSql.Club;
                        existingStanding.ClubLogo = standingSql.ClubLogo;
                        existingStanding.GamesPlayed = standingSql.GamesPlayed;
                        existingStanding.GoalsDiff = standingSql.GoalsDiff;
                        existingStanding.GoalsFor = standingSql.GoalsFor;
                        existingStanding.GoalsAgainst = standingSql.GoalsAgainst;
                        existingStanding.Won = standingSql.Won;
                        existingStanding.Lost = standingSql.Lost;
                        existingStanding.Drawn = standingSql.Drawn;
                        existingStanding.Form = standingSql.Form;
                        existingStanding.LastUpdated = standingSql.LastUpdated;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
                await _context.SaveChangesAsync();
            }
            return true;
        }
        public async Task<List<LeagueStandingSql>> GetLeagueTable(int leagueid)
        {
            await TrackApiCall(0, leagueid, DateTime.Now, DateTime.Now, "GetLeagueTable");
            if (DateTime.Now.DayOfYear - _context.LeagueStandings.Where(league => league.Leagueid == leagueid).First().LastUpdated.DayOfYear >= 1)
            {
                //if the league was last updated more than a day ago, update, else return the table
                var leaguesModel = await _matchesApiService.GetLeagueTable(leagueid);
                await UpdateLeagueTable(leaguesModel);
            }
            return _context.LeagueStandings.Where(league => league.Leagueid == leagueid).ToList();
        }
        public async Task<bool> UpdtaeFixtures(ResponseModel<Match> MatchesModel)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Fixtures ON");

                    foreach (var fixture in MatchesModel.Response)
                    {
                        try
                        {
                            var fixtureSql = new FixtureSql(fixture);

                            var existingMatch = await _context.Fixtures.FirstOrDefaultAsync(fix => fix.Id == fixture.Fixture.id);
                            if (existingMatch == null)
                            {
                                await _context.Fixtures.AddAsync(fixtureSql);
                            }
                            else
                            {
                                existingMatch.Leagueid = fixtureSql.Leagueid;
                                existingMatch.Referee = fixtureSql.Referee;
                                existingMatch.Timestamp = fixtureSql.Timestamp;
                                existingMatch.Timezone = fixtureSql.Timezone;
                                existingMatch.FixtureDate = fixtureSql.FixtureDate;
                                existingMatch.FixtureRound = fixtureSql.FixtureRound;
                                existingMatch.Season = fixtureSql.Season;
                                existingMatch.Venue = fixtureSql.Venue;
                                existingMatch.Status = fixtureSql.Status;
                                existingMatch.HomeWinOdd = fixtureSql.HomeWinOdd;
                                existingMatch.AwayWinOdd = fixtureSql.AwayWinOdd;
                                existingMatch.DrawOdd = fixtureSql.DrawOdd;
                            }

                            await _context.SaveChangesAsync(); // Save changes for each iteration to avoid DataReader issues
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            await transaction.RollbackAsync();
                            return false;
                        }
                    }

                    await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Fixtures OFF");
                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Fixtures OFF");
                    await transaction.RollbackAsync();
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
        }


        public async Task<bool> UpdtaeClubInFixtures(ResponseModel<Match> MatchesModel, bool Home)
        {
            foreach (var fixture in MatchesModel.Response)
            {
                try
                {
                    var clubInFixtureSql = ClubInFixtureSql.CreateFromMatch(fixture, Home);

                    var existingClubQuery = Home ?
                        _context.ClubsInFixtures.Where(club => club.Fixtureid == fixture.Fixture.id && club.Home == true) :
                        _context.ClubsInFixtures.Where(club => club.Fixtureid == fixture.Fixture.id && club.Home == false);

                    var existingClubs = existingClubQuery;

                    if (existingClubs.Count() == 0)
                    {
                        await _context.ClubsInFixtures.AddAsync(clubInFixtureSql);
                    }
                    else
                    {
                        Console.WriteLine(existingClubs.Count());
                        var existingClub = existingClubs.FirstOrDefault();

                        existingClub.Name = clubInFixtureSql.Name;
                        existingClub.Country = clubInFixtureSql.Country;
                        existingClub.Logo = clubInFixtureSql.Logo;
                        existingClub.Flag = clubInFixtureSql.Flag;
                        existingClub.Home = clubInFixtureSql.Home;
                        existingClub.GoalsScored = clubInFixtureSql.GoalsScored;
                        existingClub.Fixtureid = clubInFixtureSql.Fixtureid;
                        existingClub.LastUpdated = clubInFixtureSql.LastUpdated;
                    }

                    await _context.SaveChangesAsync(); // Save changes after each iteration
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            return true;
        }


        public async Task<List<FixtureAndTeams>> GetFixturesByLeague(int leagueid, DateTime startDate, DateTime endDate)
        {

            List<FixtureAndTeams> response = new List<FixtureAndTeams>();

            if (CheckFixtures(leagueid, startDate, endDate))
            {
                var fixtureModel = await _matchesApiService.GetAllMatchesByLeague(leagueid, startDate, endDate);

                var fixturesUpdated = await UpdtaeFixtures(fixtureModel);
                if (!fixturesUpdated)
                {
                    // Handle error if fixtures update failed
                    return response;
                }

                Console.WriteLine("finished fixtures");

                var homeUpdated = await UpdtaeClubInFixtures(fixtureModel, true);
                if (!homeUpdated)
                {
                    // Handle error if home clubs update failed
                    return response;
                }

                Console.WriteLine("finished home");

                var awayUpdated = await UpdtaeClubInFixtures(fixtureModel, false);
                if (!awayUpdated)
                {
                    // Handle error if away clubs update failed
                    return response;
                }

                Console.WriteLine("finished away");
            }
            var requestedFixtures = _context.Fixtures
                .Where(f => f.FixtureDate >= startDate && f.FixtureDate <= endDate.AddDays(1) && f.Leagueid == leagueid).ToList();
            Console.WriteLine($"\n\n\n\n\n\n\nrequestedFixtures.Count: {requestedFixtures.Count}\n\n\n\n\n\n\n");
            foreach (var fixture in requestedFixtures)
            {
                var homeClub = await _context.ClubsInFixtures
                    .FirstOrDefaultAsync(club => club.Fixtureid == fixture.Id && club.Home == true);
                var awayClub = await _context.ClubsInFixtures
                    .FirstOrDefaultAsync(club => club.Fixtureid == fixture.Id && club.Home == false);
                response.Add(new FixtureAndTeams(fixture, homeClub, awayClub));
            }

            await TrackApiCall(0, leagueid, startDate, endDate, "GetFixturesByLeague");
            return response;
        }
        public async Task<bool> UpdateLineup(ResponseModel<LineupModel> lineupsModel)
        {
            int fixtureId = int.Parse(lineupsModel.Parameters.Fixture);
            var homeClub = _context.ClubsInFixtures.Where(club => club.Fixtureid == fixtureId && club.Home == true).FirstOrDefault();
            var awayClub = _context.ClubsInFixtures.Where(club => club.Fixtureid == fixtureId && club.Home == false).FirstOrDefault();

            foreach (var lineup in lineupsModel.Response)
            {
                try
                {
                    int clubInFixture = lineup.Team.Name == homeClub.Name ? homeClub.id : lineup.Team.Name == awayClub.Name ? awayClub.id : 0;
                    foreach (var player in lineup.StartXI)
                    {
                        var lineupSql = new LineupSql(player.Player, clubInFixture);
                        await _context.Lineups.AddAsync(lineupSql);
                    }
                    foreach (var player in lineup.Substitutes)
                    {
                        var lineupSql = new LineupSql(player.Player, clubInFixture);
                        await _context.Lineups.AddAsync(lineupSql);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<List<LineupSql>>> GetLineup(int fixtureId)
        {
            List<List<LineupSql>> response = new List<List<LineupSql>>();
            if(!_context.Fixtures.Any(fixture=>fixture.Id == fixtureId))
            {
                return response;
            }
            var existingLineups = _context.Lineups
                .Where(lineup => lineup.ClubsInFixturesId == fixtureId * 10
                                 || lineup.ClubsInFixturesId == fixtureId * 10 + 1)
                .ToList();

            if (existingLineups.Count == 0)
            {
                var lineupModel = await _matchesApiService.GetLineups(fixtureId);
                await UpdateLineup(lineupModel);

                // Re-fetch the lineups from the database after update
                existingLineups = _context.Lineups
                    .Where(lineup => lineup.ClubsInFixturesId == fixtureId * 10
                                  || lineup.ClubsInFixturesId == fixtureId * 10 + 1)
                    .ToList();
            }

            response.Add(existingLineups.Where(lineup => lineup.ClubsInFixturesId % 2 == 0).ToList());//seperate home and away
            response.Add(existingLineups.Where(lineup => lineup.ClubsInFixturesId % 2 == 1).ToList());
            await TrackApiCall(fixtureId, 0, DateTime.Now, DateTime.Now, "GetLineup");
            return response;
        }
        public async Task<bool> UpdateEvents(ResponseModel<EventModel> eventModel)
        {
            int fixtureId = int.Parse(eventModel.Parameters.Fixture);
            var homeClub = _context.ClubsInFixtures.Where(club => club.Fixtureid == fixtureId && club.Home == true).FirstOrDefault();
            var awayClub = _context.ClubsInFixtures.Where(club => club.Fixtureid == fixtureId && club.Home == false).FirstOrDefault();
            foreach (var _event in eventModel.Response)
            {
                try
                {
                    int clubInFixture = _event.Team.Name == homeClub.Name ? homeClub.id : _event.Team.Name == awayClub.Name ? awayClub.id : 0;
                    var newEvent = new EventSql(_event, clubInFixture);
                    await _context.Events.AddAsync(newEvent);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<List<EventSql>>> GetEvents(int fixtureId)
        {
            List<List<EventSql>> response = new List<List<EventSql>>();
            if (!_context.Fixtures.Any(fixture => fixture.Id == fixtureId))
            {
                return response;
            }
            var existingEvents = _context.Events
                .Where(lineup => lineup.ClubsInFixturesId == fixtureId * 10
                                 || lineup.ClubsInFixturesId == fixtureId * 10 + 1)
                .ToList();

            if (existingEvents.Count == 0)
            {
                var eventsModel = await _matchesApiService.GetEvents(fixtureId);
                await UpdateEvents(eventsModel);

                // Re-fetch the events from the database after update
                existingEvents = _context.Events
                    .Where(_event => _event.ClubsInFixturesId == fixtureId * 10
                                  || _event.ClubsInFixturesId == fixtureId * 10 + 1)
                    .ToList();
            }

            response.Add(existingEvents.Where(_event => _event.ClubsInFixturesId % 2 == 0).ToList());//seperate home and away
            response.Add(existingEvents.Where(_event => _event.ClubsInFixturesId % 2 == 1).ToList());
            await TrackApiCall(fixtureId, 0, DateTime.Now, DateTime.Now, "GetEvents");
            return response;
        }
        public bool CheckFixtures(int leagueId, DateTime startDate, DateTime endDate)
        {
            // Check if there is need to pull from API or not
            // Get the current time and the threshold time for the last API call
            DateTime now = DateTime.Now;
            DateTime thresholdTime = now.AddMinutes(-15);

            // Fetch the most recent API call log for the specified league
            var lastApiCall = _context.ApiCallLogs
                .Where(req => req.Leagueid == leagueId)
                .OrderByDescending(req => req.Received)
                .FirstOrDefault();

            // If there are no previous API calls, pull fixtures from the API
            if (lastApiCall == null)
            {
                return true;
            }

            // If the end date today and the last API call was more than 15 minutes ago, pull fixtures from the API
            if (endDate > now.AddDays(1) && lastApiCall.Received <= thresholdTime)
            {
                return true;
            }


            // If the end date is more than a day in the past and there is an existing API call log that covers the date range, do not pull fixtures from the API
            if (endDate < now.AddDays(-1) && _context.ApiCallLogs.Any(req =>
                    req.Leagueid == leagueId && req.StartDate <= startDate && req.EndDate >= endDate))
            {
                var existingRequest = _context.ApiCallLogs.Where(req =>
                    req.Leagueid == leagueId && req.StartDate <= startDate && req.EndDate >= endDate).First();
                Console.WriteLine($"\n\n\n\nThere was already a request for this league and time range on {existingRequest.Received}\n" +
                    $"with params: StartDate - {existingRequest.StartDate} EndDate - {existingRequest.EndDate} LeagueId - {existingRequest.Leagueid}\n\n\n");
                return false;
            }

            // In all other cases, don't pull fixtures from the API
            return false;

        }
        public async Task TrackApiCall(int fixtureId, int leagueId, DateTime startDate, DateTime endDate, string method)
        {
            var apiCallLog = new ApiCallLogSql
            {
                Fixtureid = fixtureId,
                Leagueid = leagueId,
                StartDate = startDate,
                EndDate = endDate,
                Received = DateTime.Now,
                Method = method
            };

            _context.ApiCallLogs.Add(apiCallLog);
            await _context.SaveChangesAsync();
        }

    }
}
