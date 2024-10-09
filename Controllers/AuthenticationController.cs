using API_School_own_prj.Data;
using API_School_own_prj.Models.Entities;
using API_School_own_prj.ServicesManager;
using AutoMapper;
using API_School_own_prj.ServicesManager;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API_School_own_prj.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly SchoolContext _context;
        private readonly IMapper _mapper;

        public AuthenticationController(IMapper mapper, SchoolContext context)
        {
            _mapper = mapper;
            _context = context;
            
        }
        public class AuthenticationRequestBody { 
            public string? Username { get; set; }
            public string? Password { get; set; }
        }

        
        
        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {
            User? user = ValidateUserCredential(authenticationRequestBody.Username, authenticationRequestBody.Password);
            if (user == null) return NotFound();

            

            return Ok(user);
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
