using API_School_own_prj.Data;
using API_School_own_prj.Models.DTOs;
using API_School_own_prj.Models.Entities;
using API_School_own_prj.ServicesManager;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_School_own_prj.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly SchoolContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IMapper mapper, SchoolContext context, IConfiguration configuration)
        {
            _mapper = mapper;
            _context = context;
            _configuration = configuration;
        }
        public class AuthenticationRequestBody { 
            public string? Username { get; set; }
            public string? Password { get; set; }
        }

        
        
        [HttpPost("authenticate")]
        public ActionResult<InloggedUserDto> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {
            User? user = ValidateUserCredential(authenticationRequestBody.Username, authenticationRequestBody.Password);
            if (user == null) return Unauthorized();
            
            string? sKey = (_configuration?["secretKey"]) ?? throw new ArgumentNullException("something went wrong");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(sKey));
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> infoInToken =
            [
                new Claim("Name", user.Name!),
                new Claim("Surname", user.Surname!),
                new Claim("Role", user.Role!),
                new Claim("Email", user.Email!.ToString()),
                new Claim("Id", user.Id!.ToString()),
            ];

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["JwtSettings:Issuer"],
                _configuration["JwtSettings:Audience"],
                infoInToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:Expires"])),
                credential);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            var loggedUser = _mapper.Map<InloggedUser>(user);
            loggedUser.Token = tokenToReturn;

            var loggedUserDto = _mapper.Map<InloggedUserDto>(loggedUser);

            return Ok(loggedUserDto);
        }

        private User? ValidateUserCredential(string? username, string? password)
        {
            User? existingUser = _context.Teachers.Where(t => t.UserName == username).FirstOrDefault();
            
            if (existingUser == null) { 
                existingUser = _context.Students.Where(t => t.UserName == username).FirstOrDefault();
            }

            if (existingUser == null)
                return null;

            var authenticated = PasswordHasher.VerifyHashedPassword(existingUser.PasswordHash!, password!);

            return authenticated ? existingUser: null;

        }
    }
}
