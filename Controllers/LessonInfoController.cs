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
    public class LessonInfoController : Controller
    {
        DairyContext dbContext = new DairyContext();

        private readonly IMapper _mapper;

        public LessonInfoController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Course> cources = await dbContext
                .Course
                .Include(course => course.Subject)
                .Include(specialization => specialization.Specialization)
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<CourseVM>>(cources);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            IEnumerable<LessonInfo> info = await dbContext
                .LessonInfo
                .Where(i=>i.UserId == id)
                .Include(i=>i.Schedule)
                .ThenInclude(i=>i.Subject)
                .Include(i=>i.User)
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<LessonInfoVM>>(info);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetForTeacher(int id)
        {
            IEnumerable<LessonInfo> info =await dbContext
                .LessonInfo
                .Include(i=>i.Schedule)
                .ThenInclude(i => i.Subject)
                .Include(i => i.Schedule.Group)
                .Where(i => i.Schedule.TeacherId == id)
                .Include(i => i.User)
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<LessonInfoVM>>(info);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLessonInfoByGroup(int id)
        {
            var student = await dbContext.Student.FirstOrDefaultAsync(i => i.UserId == id);

            IEnumerable<LessonInfo> info = await dbContext
                .LessonInfo
                .Where(i => i.Schedule.GroupId == student.GroupId)
                .Include(i => i.Schedule)
                .ThenInclude(i => i.Subject)
                .Include(i => i.User)
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<LessonInfoVM>>(info);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] List<LessonInfo> lessonInfo)
        {
            foreach (var info in lessonInfo)
            {
                await Task.Run(()=>dbContext.LessonInfo.Add(info));
            }    
            
            await Task.Run(()=>dbContext.SaveChanges());

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var lessonInfo = await dbContext
                .LessonInfo
                .FirstOrDefaultAsync(i => i.Id == id);

            if (lessonInfo != null)
            {
                await Task.Run(() => dbContext.LessonInfo.Remove(lessonInfo));

                await Task.Run(() => dbContext.SaveChanges());

                return Ok();
            }

            return BadRequest(new {message = "Can't delete lesson info"});
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] LessonInfo item)
        {
            var lessonInfo = await dbContext
                .Course
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == item.Id);

            if (lessonInfo != null)
            {
                await Task.Run(() => dbContext.Course.Update(lessonInfo));

                await Task.Run(() => dbContext.SaveChanges());

                return Ok();
            }

            return BadRequest(new {message = "Can't edit lesson info"});
        }
    }
}