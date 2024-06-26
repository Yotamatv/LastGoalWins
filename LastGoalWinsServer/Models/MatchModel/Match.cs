﻿using LastGoalWinsServer.Models.General;

namespace LastGoalWinsServer.Models.MatchModel
{
    public class Match
    {
        public Fixture Fixture { get; set; }
        public League League { get; set; }
        public Teams Teams { get; set; }
        public Goals Goals { get; set; }
        public Score Score { get; set; }
    }
}
