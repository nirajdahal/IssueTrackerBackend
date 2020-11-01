using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Entities.Models.Users
{
    public class ChangePassword
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}