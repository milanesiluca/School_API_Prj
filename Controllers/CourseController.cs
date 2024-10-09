﻿using API_School_own_prj.Data;
using API_School_own_prj.Models.DTOs;
using API_School_own_prj.Models.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_School_own_prj.API.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly SchoolContext _context;
        public CourseController(IMapper mapper, SchoolContext context)
        {
            _mapper = mapper;
            _context = context;
            //_userManager = userManager;
            //_serviceManager = serviceManager;

        }


        //[Authorize(Roles = "Teacher")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
            var courses = await _context.Set<Course>().Include(c => c.Modules).ToListAsync();
            var courseDtos = _mapper.Map<IEnumerable<CourseDto>>(courses);

            return Ok(courseDtos);
        }


        //[Authorize(Roles = "Teacher")]
        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateCourse(CourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            _context.Set<Course>().Add(course);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<CourseDto>(course));
        }

        //[Authorize(Roles = "Teacher,Student")]
        [HttpGet("{user_id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(string user_id)
        {
            var user = await _context.Students
                .Where(u => u.Id == user_id)
                .Select(u => new { u.CourseId })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (!user.CourseId.HasValue)
            {
                return NotFound("User does not have an assigned course.");
            }

            var course = await _context.Courses
                .Include(c => c.Modules)
                .FirstOrDefaultAsync(c => c.Id == user.CourseId);

            if (course == null)
            {
                return NotFound("Course not found.");
            }

            var courseDto = _mapper.Map<CourseDto>(course);
            return Ok(courseDto);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var course = await _context.Set<Course>()
                .Include(c => c.Modules)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CourseDto>> UpdateCourse(string id, CourseForUpdateDto courseDto)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id.ToString() == id);
            if (course == null)
            {
                return NotFound();
            }
            _mapper.Map(courseDto, course);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<CourseForUpdateDto>(course));

        }
    }
}
