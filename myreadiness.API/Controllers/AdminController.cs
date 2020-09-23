using myreadiness.API.Data;
using myreadiness.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using myreadiness.API.Dtos.RoleDtos;
using myreadiness.API.Models;
using System;

namespace myreadiness.API.Controllers
{
    /*
     * Developer: Jinny Saw
     * DateTime: 2019-Sept-16
     */
     [ApiController]
    [Route ("api/[controller]")]
    public class AdminController : ControllerBase {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        public AdminController (DataContext context, UserManager<User> userManager) {
            _userManager = userManager;
            _context = context;
        }

        [Authorize (Policy = "RequireAdminRole")]
        [HttpGet ("usersWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles () {
            var userlist = await (from user in _context.Users orderby user.UserName select new {
                Id = user.Id,
                    UserName = user.UserName,
                    Roles = (from userRole in user.UserRoles join role in _context.Roles on userRole.RoleId equals role.Id select role.Name).ToList ()
            }).ToListAsync ();
            return Ok (userlist);
        }

        [HttpGet ("AdminUsers")]
        public async Task<IActionResult> GetAdminUsers () {
            var userlist = await (from user in _context.Users
                                  join userRole in _context.UserRoles on user.Id equals userRole.UserId
                                  join role in _context.Roles on userRole.RoleId equals role.Id where role.Name == "Admin" && user.Email !=null select user.Email).ToListAsync();
            return Ok (userlist);
        }

        [Authorize (Policy = "RequireAdminRole")]
        [HttpGet ("usersWithRolesPaging")]
        public async Task<IActionResult> GetUsersWithRolesPagination ([FromQuery] UserParams objParams) {
            var userlist =  (from user in _context.Users.Include(u => u.Employee).Where(u => u.UserName !="Admin") orderby user.UserName select new {
                Id = user.Id,
                    UserName = user.UserName,
                    Employee = user.Employee.EmpName,
                    EmpId = user.EmpId, 
                    Roles = (from userRole in user.UserRoles join role in _context.Roles 
                            on userRole.RoleId equals role.Id 
                            select role.Name).ToList ()
            });

            if (objParams.EmpID != Guid.Empty)
            {
                userlist = userlist.Where(u => u.EmpId == objParams.EmpID);
            }

            var users =  await PagedList<object>.CreateAsync(userlist, objParams.PageNumber, objParams.PageSize);
            Response.AddPagination (users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
            
            return Ok (users);
        }
    
        [Authorize (Policy = "RequireAdminRole")]
        [HttpPost ("editRoles/{userName}")]
        public async Task<IActionResult> EditRoles (string userName, RoleEditDto roleEditDto) 
        {
                var user = await _userManager.FindByNameAsync(userName);

                var userRoles = await _userManager.GetRolesAsync(user);

                var selectedRoles = roleEditDto.RoleNames;

                selectedRoles = selectedRoles ?? new string[] { };
                var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

                if (!result.Succeeded)
                    return BadRequest("Failed to add to roles");

                result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

                if (!result.Succeeded)
                    return BadRequest("Failed to remove the roles");

                return Ok(await _userManager.GetRolesAsync(user)); 
        }
    }
}