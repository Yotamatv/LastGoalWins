﻿using System;
using LastGoalWinsServer.Models.MatchModel;

namespace LastGoalWinsServer.Models.General
{
    public class Fixture
    {
        public int id { get; set; }
        public string? Referee { get; set; }
        public string? Timezone { get; set; }
        public DateTime Date { get; set; }
        public long Timestamp { get; set; }
        public Periods? Periods { get; set; }
        public Venue? Venue { get; set; }
        public Status? Status { get; set; }
    }
}
