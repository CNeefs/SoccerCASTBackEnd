using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SoccerCASTBackEnd.Models {
    public class User {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [NotMapped]
        public string Token { get; set; }

        public DateTime BirthDate { get; set; }
        public int TimesWon { get; set; }
        public int TimesLost { get; set; }

        [NotMapped]
        public List<Role> Roles { get; set; }

        [NotMapped]
        public List<Team> Teams { get; set; }

        [NotMapped]
        public List<string> Permissions { get; set; }
    }
}
