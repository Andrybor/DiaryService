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
    public class SubjectController : ControllerBase
    {
        DairyContext dbContext = new DairyContext();

        private readonly IMapper _mapper;

        public SubjectController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Subject> subjects = await dbContext
                .Subject
                .Include(course => course.Course)
                .ToListAsync(); 

            var result = _mapper.Map<IEnumerable<SubjectVM>>(subjects);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Subject subject)
        {
            await Task.Run(()=>dbContext.Subject.Add(subject));

            await Task.Run(() => dbContext.SaveChanges());

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var subject = await dbContext.Subject.FirstOrDefaultAsync(i => i.Id == id);
            if (subject != null)
            {
                await Task.Run(()=>dbContext.Subject.Remove(subject));

                await Task.Run(() => dbContext.SaveChanges());

                return Ok();
            }

            return BadRequest(new {message = "Can't delete subject"});
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Subject item)
        {
            var subject = await dbContext.Subject.FirstOrDefaultAsync(i => i.Id == item.Id);
            if (subject != null)
            {
                subject.Title = item.Title;
                subject.CourseId = item.CourseId;

                await Task.Run(()=>dbContext.Subject.Update(subject));

                await Task.Run(()=>dbContext.SaveChanges());

                return Ok();
            }

            return BadRequest(new {message = "Can't edit subject"});
        }
    }
}