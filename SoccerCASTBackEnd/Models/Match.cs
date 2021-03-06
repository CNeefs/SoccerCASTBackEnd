﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerCASTBackEnd.Models
{
    public class Match
    {
        public int MatchID { get; set; }
        public int Score1 { get; set; }
        public int Score2 { get; set; }
        public DateTime Date { get; set; }

        //relations
        public int TableID { get; set; }
        public Table Table {get;set;}
        public int MatchTypeID { get; set; }
        public MatchType MatchType { get; set; }
        public int? Team1ID { get; set; }
        public Team Team1 { get; set; }
        public int? Team2ID { get; set; }
        public Team Team2 { get; set; }
        public int? Player1ID { get; set; }
        public User Player1 { get; set; }
        public int? Player2ID { get; set; }
        public User Player2 { get; set; }
        public int? Player3ID { get; set; }
        public User Player3 { get; set; }
        public int? Player4ID { get; set; }
        public User Player4 { get; set; }
        public int MatchStatusID { get; set; }
        public MatchStatus MatchStatus { get; set; }
        public int? CompetitionID { get; set; }
        public Competition Competition { get; set; }
        public int? TournamentID { get; set; }
        public Tournament Tournament { get; set; }
        public int? Round { get; set; }
        public int? Number { get; set; }
        public int? NextRound { get; set; }

    }
}
