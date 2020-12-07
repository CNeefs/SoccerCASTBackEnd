using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerCASTBackEnd.Models
{
    public class RolePermission
    {
        public int RolePermissionID { get; set; }
        //relations
        public int RoleID { get; set; }
        public Role Role { get; set; }
        public int PermissionID { get; set; }
        public Permission Permission { get; set; }
    }
}
