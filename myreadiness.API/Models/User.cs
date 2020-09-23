using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace myreadiness.API.Models
{
    public class User : IdentityUser<Guid>
    {
        public Guid? EmpId { get; set; }
        public Employee Employee { get; set; }
        public string KnownAs {get; set;}
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
