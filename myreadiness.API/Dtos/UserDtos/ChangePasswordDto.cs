using System.ComponentModel.DataAnnotations;

namespace myreadiness.API.Dtos.UserDtos
{
    public class ChangePasswordDto
    {
        [Required]
        public string currentPassword;
        [Required]
        public string newPassword;
    }
}