using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerCASTBackEnd.Models
{
    public class Competition
    {
        public int CompetitionID { get; set; }
        public string Name { get; set; }
        public bool isActive { get; set; }
    }
}
