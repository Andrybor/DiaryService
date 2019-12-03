using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleDataService.BAL;
using SimpleDataService.DAL;

namespace SimpleDataService.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        DairyContext dbContext = new DairyContext();

        private readonly IMapper _mapper;

        public ScheduleController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Schedule> schedules = await dbContext
                .Schedule
                .Include(i=>i.Subject)
                .Include(i => i.Auditorium)
                .Include(i => i.Group)
                .Include(i => i.Teacher)
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<ScheduleVM>>(schedules);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var student = await dbContext
                .Student
                .FirstOrDefaultAsync(i => i.UserId == id);

            if (student != null)
            {
                IEnumerable<Schedule> schedules =await dbContext
                    .Schedule
                    .Where(i => i.Group.Id == student.GroupId)
                    .Include(i => i.Subject)
                    .Include(i => i.Auditorium)
                    .Include(i => i.Group)
                    .Include(i => i.Teacher)
                    .ToListAsync(); 

                return Ok(_mapper.Map<IEnumerable<ScheduleVM>>(schedules));
            }
            else
            {
                var teacher = await dbContext.User.FirstOrDefaultAsync(i => i.Id == id);

                if (teacher != null)
                {
                    IEnumerable<Schedule> schedules = await dbContext
                        .Schedule
                        .Where(i => i.TeacherId == teacher.Id)
                        .Include(i => i.Subject)
                        .Include(i => i.Auditorium)
                        .Include(i => i.Group)
                        .Include(i => i.Teacher)
                        .ToListAsync();

                    return Ok(_mapper.Map<IEnumerable<ScheduleVM>>(schedules));
                }
            }

            return BadRequest(new {message = "Schedule not found"});
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Schedule schedule)
        {
            var schedules = await dbContext.Schedule.Where(i => i.StartTime < schedule.StartTime && i.EndTime > schedule.EndTime).ToListAsync();
            foreach (var item in schedules)
            {
                if (item.TeacherId == schedule.TeacherId)
                {
                    return BadRequest(new {message = "This teacher have lesson in this time"});
                }

                if (item.AuditoriumId == schedule.AuditoriumId)
                {
                    return BadRequest(new { message = "This Auditory is full on this time" });
                }
            }

            await Task.Run(() => dbContext.Schedule.Add(schedule));

            await Task.Run(() => dbContext.SaveChanges());

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var schedule = await dbContext
                .Schedule
                .FirstOrDefaultAsync(i => i.Id == id);

            if (schedule != null)
            {
                await Task.Run(() => dbContext.Schedule.Remove(schedule));

                await Task.Run(() => dbContext.SaveChanges());

                return Ok();
            }

            return BadRequest(new {message = "Can't delete Schedule"});
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Schedule item)
        {
            if (item != null)
            {
               await Task.Run(()=>dbContext.Schedule.Update(item));

               await Task.Run(() => dbContext.SaveChanges());

               return Ok();
            }

            return BadRequest(new {message = "Schedule can't edit"});
        }
    }
}