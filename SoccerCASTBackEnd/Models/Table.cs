using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerCASTBackEnd.Models
{
    public class Table
    {
        public int TableID { get; set; }
        public string TableName { get; set; }
        public string? CompanyName { get; set; }
        public string Location { get; set; }
        
        //relations
        public int ContactUserID { get; set; }
        public User ContactUser { get; set; }
        public int TableStatusID { get; set; }
        public TableStatus TableStatus { get; set; }
    }
}
