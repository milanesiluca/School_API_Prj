using API_School_own_prj.Data;
using API_School_own_prj.Models.DTOs;
using API_School_own_prj.Models.Entities;
using API_School_own_prj.ServicesManager;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_School_own_prj.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly SchoolContext _context;

        public StudentsController (IMapper mapper, SchoolContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("courseid/{id}")]
        public async Task<ActionResult<IEnumerable<StudentForListDto>>> GetStudent(Guid id) {
            var users = await _context.Students
                                      .Where(st => st.CourseId == id)
                                      .ToListAsync();

            if (users == null) return NotFound();

            List<StudentForListDto> students = [];
            foreach (var user in users) { 
                var dtoObj = _mapper.Map<StudentForListDto>(user);
                students.Add(dtoObj);
            }

            return(students);
        }

        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser(StudentDto studentDto) {

            if (studentDto.CourseID == null) return BadRequest("Course required to regiter a student");

            var courseExist = await _context.Courses.Where(cour => cour.Id.ToString() == studentDto.CourseID).FirstOrDefaultAsync();
            if (courseExist == null) return BadRequest("The selected course ");

            var dto = _mapper.Map<Student>(studentDto);

            dto.PasswordHash = PasswordHasher.HashPassword(studentDto.Password);

            _context.Students.Add(dto);
            var result = await _context.SaveChangesAsync();

            return result > 0 
                ? StatusCode(StatusCodes.Status201Created)
                : BadRequest("Something went wrong");
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveUser(Guid id) {

            var userToRemove = await _context.Students.FindAsync(id);

            if (userToRemove == null) return BadRequest();

            _context.Students.Remove(userToRemove);
            var result = await _context.SaveChangesAsync();

            return result > 0
                ? NoContent()
                : BadRequest("Something went wrong");
        }
    }
}
