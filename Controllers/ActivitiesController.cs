using API_School_own_prj.Data;
using API_School_own_prj.Models.DTOs;
using API_School_own_prj.Models.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_School_own_prj.API.Controllers
{
    [Route("api/activities")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly SchoolContext _context;
        private readonly IMapper _mapper;

        public ActivitiesController(IMapper mapper, SchoolContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/Activities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityListDto>>> GetActivities()
        {
            var activitiesList = await _context.Activities
                .Include(act => act.Type)
                .ToListAsync();

            List<ActivityListDto> dto = new();
            foreach (var activity in activitiesList)
            {
                var dtoObj = _mapper.Map<ActivityListDto>(activity);
                dto.Add(dtoObj);
            }
            return dto;
        }

        // GET: api/Activities/5
        [HttpGet("moduleid/{id}")]
        public async Task<ActionResult<IEnumerable<ActivityListDto>>> GetActivityByModuleId(Guid id)
        {
            var actList = await _context.Activities.Where(act => act.ModuleId == id).Include(act => act.Type).ToListAsync();

            if (actList == null) return NotFound();

            List<ActivityListDto> dto = new();
            foreach (var activity in actList)
            {
                var dtoObj = _mapper.Map<ActivityListDto>(activity);
                dto.Add(dtoObj);
            }

            return dto;
        }

        // PUT: api/Activities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity(Guid id, ActivityPutDto activity)
        {
            var actObj = _context.Activities.Where(actObj => actObj.Id == id).FirstOrDefault();

            if (actObj == null) return BadRequest();

            if (id != actObj.Id) return BadRequest();

            //_mapper.Map(module, moduleObj);

            _mapper.Map(activity, actObj);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Activities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ActivityListDto>> PostActivity(ActivityPutDto activity)
        {
            var actObj = _mapper.Map<Activity>(activity);
            _context.Activities.Add(actObj);
            await _context.SaveChangesAsync();

            return Created();
        }

        // DELETE: api/Activities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActivityExists(Guid id)
        {
            return _context.Activities.Any(e => e.Id == id);
        }
    }
}
