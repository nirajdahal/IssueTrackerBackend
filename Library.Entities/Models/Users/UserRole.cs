using System;
using System.Collections.Generic;

namespace Library.Entities.Models
{
    public class UserRole
    {
        public string Type { get; set; }
    }

    public class UserRoleForModification
    {
        public Guid UserId { get; set; }

        public string Role{ get; set; }
    }

    public class CreateRoles
    {
        public List<string> Roles { get; set; }
       
    }
}
