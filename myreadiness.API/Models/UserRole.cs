using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace myreadiness.API.Models
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public User User {get; set;}
        public Role Role { get; set; }
    }
}