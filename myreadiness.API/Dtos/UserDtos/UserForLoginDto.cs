using System.ComponentModel.DataAnnotations;

namespace myreadiness.API.Dtos.UserDtos
{
    public class UserForLoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}