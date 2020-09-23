using System;
using System.ComponentModel.DataAnnotations;

namespace myreadiness.API.Dtos.UserDtos
{
    public class UserForUpdateDto
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public DateTime LastActive { get; set; }

        public UserForUpdateDto() 
        {
            LastActive = DateTime.Now;            
        }
    }
}