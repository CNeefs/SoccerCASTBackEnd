using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerCASTBackEnd.Models
{
    public class UserTeam
    {
        public int UserTeamID { get; set; }

        //relations
        public int? UserID { get; set; }
        public User User { get; set; }
        public int? TeamID { get; set; }
        public Team Team { get; set; }
    }
}
