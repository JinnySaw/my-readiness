using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace myreadiness.API.Models
{
    public class Role: IdentityRole<Guid>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}