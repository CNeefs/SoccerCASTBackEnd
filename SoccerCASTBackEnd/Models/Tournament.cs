using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerCASTBackEnd.Models
{
    public class Tournament
    {
        public int TournamentID { get; set; }
        public string Edition { get; set; }
        public int? Total_Joined { get; set; }
        public int Match_Count { get; set; }
        public bool isStart { get; set; }
        public string? Winner { get; set; }

        //relations
        public int TournamentStatusID { get; set; }
        public TournamentStatus TournamentStatus { get; set; }
    }
}
