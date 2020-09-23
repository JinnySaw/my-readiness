using System;

namespace myreadiness.API.Dtos.UserDtos
{
    public class UserForListDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public Guid? EmpID {get;set;}
        public string Email {get; set;}
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string EmployeeName { get; set; }
    }
}