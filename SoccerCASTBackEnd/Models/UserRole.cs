using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerCASTBackEnd.Models
{
    public class UserRole
    {
        public int UserRoleID { get; set; }
        //relations
        public int UserID { get; set; }
        public User User { get; set; }
        public int RoleID { get; set; }
        public Role Role { get; set; }
    }
}
