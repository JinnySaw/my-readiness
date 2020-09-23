using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using myreadiness.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using myreadiness.API.Dtos.UserDtos;
using Microsoft.AspNetCore.Identity;
using myreadiness.API.Models;
using myreadiness.API.Data;
using Microsoft.EntityFrameworkCore;

namespace myreadiness.API.Controllers
{
    /*
     * Developer: Jinny Saw
     * DateTime: 2019-Sept-16
     */
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserRepository _repoUser;

        //private readonly IEmployeeRepository _repoEmp;
        private readonly IDomainRepository _repoDomainAddress;
        //private readonly INotificationRepository _notiRepo;

        public AuthController(IConfiguration config,
            IMapper mapper,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IUserRepository repoUser,
            IDomainRepository repoDomainAddress)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _repoUser = repoUser;
            _repoDomainAddress = repoDomainAddress;
            _mapper = mapper;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {

            var user = await _repoUser.GetUserByEmpId(userForRegisterDto.EmpID);
            if (user != null)
            {
                return BadRequest("User already exist");
            }

            //var employee = await _repoEmp.GetEmployee(userForRegisterDto.EmpID);
            //if (employee == null)
            //{
            //    return BadRequest("Employee doesn't exist!");
            //}

            var userToCreate = _mapper.Map<User>(userForRegisterDto);
            //userToCreate.Employee = employee;
            //userToCreate.EmpId = employee.Id;

            var result = await _userManager.CreateAsync(userToCreate, userForRegisterDto.Password);

            if (result.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(userToCreate, "Member");
                var userToReturn = _mapper.Map<UserForDetailDto>(userToCreate);
                if (userForRegisterDto.Email != null)
                {
                    // email to 
                    var domainaddress = await _repoDomainAddress.GetActiveDomain();
                    if (domainaddress != null)
                    {
                        string text = "Dear Sir/Madam,\n\n";
                        text += "Welcome to our Cardiac Surgery Application. \n";
                        text += "Please use this username and password to login to the system.\n\n";
                        text += "username: " + userForRegisterDto.Username + "\n";
                        text += "password: " + userForRegisterDto.Password + "\n\n";

                        text += "Application URL: " + domainaddress.DomainAddress + "\n\n";

                        text += "Best Regards,\n";
                        text += "QCST Team";
                        string subject = "Member Created Information";
                    }
                   // await _notiRepo.SendMailTo(text, subject, userForRegisterDto.Email);
                }

                return CreatedAtRoute("GetUser",
                    new { controller = "Users", id = userToCreate.Id }, userToReturn);
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            // check username and password store in db
            var user = await _userManager.FindByNameAsync(userForLoginDto.Username);
            if (user == null)
            {
                return BadRequest("User Name is incorrect!");
            }
            var result = await _signInManager
                .CheckPasswordSignInAsync(user, userForLoginDto.Password, false);

            if (result.Succeeded)
            {
                //var appUser = await _userManager.Users
                //    .FirstOrDefaultAsync(u => u.NormalizedUserName == userForLoginDto.Username.ToUpper());

                var appUser = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.NormalizedUserName == userForLoginDto.Username.ToUpper());

                var userToReturn = _mapper.Map<UserForListDto>(appUser);
                //if (appUser.EmpId.HasValue)
                //{
                //    var employee = await _repoEmp.GetEmployee((int)appUser.EmpId);
                //    userToReturn.EmployeeName = employee.EmpName;
                //}
                return Ok(new
                {
                    token = GenerateJwtToken(appUser).Result,
                    user = userToReturn
                });
            }
            return Unauthorized("Unauthorized");
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            // Build Token
            // Token contains two claims
            // the first one is userid
            // the second is name

            var claims = new List<Claim>
            {
                new Claim (ClaimTypes.NameIdentifier, user.Id.ToString ()),
                new Claim (ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // we creating security key
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));
            // signing credentials and encrypted this key with a hashing algorithm
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}