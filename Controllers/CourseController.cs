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
    public class CourseController : ControllerBase
    {
        DairyContext dbContext = new DairyContext();

        private readonly IMapper _mapper;

        public CourseController(IMapper mapper)
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Course course)
        {
            await Task.Run(() => dbContext.Course.Add(course));

            await Task.Run(() => dbContext.SaveChanges());

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var course = await dbContext
                .Course
                .FirstOrDefaultAsync(i => i.Id == id);

            if (course != null)
            {
                await Task.Run(() => dbContext.Course.Remove(course));

                await Task.Run(() => dbContext.SaveChanges());

                return Ok();
            }

            return BadRequest(new { message = "Course can't deleted" });
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Course item)
        {
            var course = await dbContext
                .Course
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == item.Id);

            if (course != null)
            {
                course.Title = item.Title;
                course.SpecializationId = item.SpecializationId;
                course.AmountOfHours = item.AmountOfHours;
               
                await Task.Run(() => dbContext.Course.Update(item));

                await Task.Run(() => dbContext.SaveChanges());

                return Ok();
            }

            return BadRequest(new { message = "Course can't edited" });
        }
    }
}