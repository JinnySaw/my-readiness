using System;
using System.ComponentModel.DataAnnotations;

namespace myreadiness.API.Dtos.UserDtos
{
    public class ResetPasswordDto
    {
        [Required]
        public Guid userId;
        [Required]
        public string newPassword;
    }
}