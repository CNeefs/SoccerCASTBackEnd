using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerCASTBackEnd.Models
{
    public class Competition
    {
        public int CompetitionID { get; set; }
        public String Name { get; set; }

        //relations
        public int TournamentID { get; set; }
        public Tournament Tournament { get; set; }
    }
}
