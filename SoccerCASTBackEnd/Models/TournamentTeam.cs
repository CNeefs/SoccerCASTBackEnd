using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerCASTBackEnd.Models
{
    public class TournamentTeam
    {
        public int TournamentTeamID { get; set; }

        //relations
        public int TournamentID { get; set; }
        public Tournament Tournament { get; set; }
        public int? TeamID { get; set; }
        public Team Team { get; set; }
        public int? Player1ID { get; set; }
        public User Player1 { get; set; }
        public int? Player2ID { get; set; }
        public User Player2 { get; set; }
    }
}
