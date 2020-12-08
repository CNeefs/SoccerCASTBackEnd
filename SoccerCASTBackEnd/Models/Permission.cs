using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SoccerCASTBackEnd.Models
{
    public class Permission
    {
        public int PermissionID { get; set; }
        public string Name { get; set; }
        //relations
        public int? RoleID { get; set; }
        public Role Role { get; set; }
    }
}
