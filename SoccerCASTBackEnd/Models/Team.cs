using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SoccerCASTBackEnd.Models
{
    public class Team
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }

        //relations
        public int CaptainID { get; set; }
        public User Captain { get; set; }
        public int TeamStatusID { get; set; }
        public TeamStatus TeamStatus { get; set; }
    }
}
