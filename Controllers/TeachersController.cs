using API_School_own_prj.Data;
using API_School_own_prj.Models.DTOs;
using API_School_own_prj.Models.Entities;
using API_School_own_prj.ServicesManager;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_School_own_prj.Controllers
{
    [Route("api/teacher")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly SchoolContext _context;

        public TeachersController(IMapper mapper, SchoolContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("moduleid/{id}")]
        public async Task<ActionResult<IEnumerable<TeacherForListDto>>> GetTeacher(Guid id)
        {
            var users = await _context.Teachers
                                      .Where(st => st.ModuleId == id)
                                      .ToListAsync();

            if (users == null) return NotFound();

            List<TeacherForListDto> students = [];
            foreach (var user in users)
            {
                var dtoObj = _mapper.Map<TeacherForListDto>(user);
                students.Add(dtoObj);
            }

            return (students);
        }

        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser(TeacherDto theacherDto)
        {

            if (theacherDto.ModuleId == null) return BadRequest("Module required to regiter a ");

            var courseExist = await _context.Modules.Where(cour => cour.Id == theacherDto.ModuleId).FirstOrDefaultAsync();
            if (courseExist == null) return BadRequest("The selected course ");
            

            var dto = _mapper.Map<Teacher>(theacherDto);
            dto.PasswordHash = PasswordHasher.HashPassword(theacherDto.Password);

            _context.Teachers.Add(dto);
            var result = await _context.SaveChangesAsync();

            return result > 0
                ? StatusCode(StatusCodes.Status201Created)
                : BadRequest("Something went wrong");
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveUser(Guid id)
        {

            var userToRemove = await _context.Teachers.FindAsync(id);

            if (userToRemove == null) return BadRequest();

            _context.Teachers.Remove(userToRemove);
            var result = await _context.SaveChangesAsync();

            return result > 0
                ? NoContent()
                : BadRequest("Something went wrong");
        }


    }
}
