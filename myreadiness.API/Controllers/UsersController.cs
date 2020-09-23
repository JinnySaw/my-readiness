using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using myreadiness.API.Dtos.UserDtos;
using myreadiness.API.Helpers;
using myreadiness.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace myreadiness.API.Controllers
{
    /*
     * Developer: Jinny Saw
     * DateTime: 2019-Sept-17 2:00 PM
     */
    [ServiceFilter (typeof (LogUserActivity))]
    [Route ("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUserRepository _repoUser;
        // private readonly UserManager<User> _userManager;
        public UsersController (IRepository repo, IUserRepository repoUser, IMapper mapper) {
            
            _repoUser = repoUser;
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers ([FromQuery] ObjectParams objParams) {
            var users = await _repoUser.GetUsers (objParams);
            var userToReturn = _mapper.Map<IEnumerable<UserForListDto>> (users);

            Response.AddPagination (users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
            return Ok (userToReturn);
        }

        [HttpGet ("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser (Guid id) {
            var user = await _repoUser.GetUser (id);

            var userToReturn = _mapper.Map<UserForListDto> (user);
            return Ok (userToReturn);
        }

        // [HttpPost("changepassword")]
        // public async Task<IActionResult> ChangePassword (ChangePasswordDto dto) {
        //     var userId = int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value);
        //     var user = await _repoUser.GetUser (userId);
        //     if(user == null) {
        //         return Unauthorized();
        //     }

        //     var currentPasswordFlg = await _userManager.CheckPasswordAsync(user, dto.currentPassword);
        //     if((bool)currentPasswordFlg) {
        //         var result = await _userManager.ChangePasswordAsync(user, dto.currentPassword, dto.newPassword);
        //         if(result.Succeeded) {
        //              return Ok();
        //         }
        //         else {
        //             return BadRequest(result.ToString());
        //         }
        //     }
        //     else {
        //         return BadRequest("Your current Password is incorrect!");
        //     }
        //     // return BadRequest();
        // }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var adminUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var admin = await _repoUser.GetUser(adminUserId);
            if (admin == null)
            {
                return Unauthorized();
            }
            var roles = await _userManager.GetRolesAsync(admin);
            var roleName = "Admin";

            if (!roles.Contains(roleName))
            {
                return Unauthorized();
            }

            var user = await _repoUser.GetUser(dto.userId);
            if (user == null)
            {
                return BadRequest("User doesn't exist");
            }

            var removeResult = await _userManager.RemovePasswordAsync(user);
            if (removeResult.Succeeded)
            {
                var result = await _userManager.AddPasswordAsync(user, dto.newPassword);
                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UserForUpdateDto userForUpdateDto)
        {
            if (id != Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repoUser.GetUser(id);

            _mapper.Map(userForUpdateDto, userFromRepo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Updating user {id} failed on save");

        }
    }
}