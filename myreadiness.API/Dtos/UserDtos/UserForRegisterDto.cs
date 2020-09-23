using System;
using System.ComponentModel.DataAnnotations;

namespace myreadiness.API.Dtos.UserDtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength =4, ErrorMessage ="You must specify password between 4 and 8 characters")]
        public string Password { get; set; }
        
        [Required]
        public Guid EmpID {get;set;}
        // public string KnownAs { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email {get; set;}
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        public UserForRegisterDto() 
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;            
        }
    }
}