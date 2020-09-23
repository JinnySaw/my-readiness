using System;
using System.Collections.Generic;
using myreadiness.API.Dtos.RoleDtos;

namespace myreadiness.API.Dtos.UserDtos
{
    public class UserRolesDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public List<RoleEditDto> Roles { get; set; }
    }
}