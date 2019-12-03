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
    public class HomeWorkController : ControllerBase
    {
        DairyContext dbContext = new DairyContext();

        private readonly IMapper _mapper;

        public HomeWorkController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Group> groups = await dbContext
                .Group
                .Include(i => i.Course)
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<GroupVM>>(groups);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var student = await dbContext.Student.FirstOrDefaultAsync(i => i.UserId == id);
            if (student != null)
            {
                var homework = dbContext
                    .Homework
                    .Include(i => i.Schedule)
                    .Where(i => i.Schedule.GroupId == student.GroupId);

                var result = _mapper.Map<IEnumerable<HomeWorkVM>>(homework);

                return Ok(result);
            }

            return BadRequest(new {message = "Homework not found" });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Homework homeWork)
        {
            if (homeWork != null)
            {
                await Task.Run(() => dbContext.Homework.Add(homeWork));

                await Task.Run(() => dbContext.SaveChanges());

                return Ok();
            }

            return BadRequest(new {message = "Homework is empty"});
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var homeWork = await dbContext.Homework.FirstOrDefaultAsync(i => i.Id == id);
            if (homeWork != null)
            {
                await Task.Run(() => dbContext.Homework.Remove(homeWork));

                await Task.Run(() => dbContext.SaveChanges());

                return Ok();
            }

            return BadRequest(new {message = "Can't delete Homework"});
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Homework item)
        {
            if (item != null)
            {
                await Task.Run(()=>dbContext.Homework.Update(item));

                await Task.Run(() => dbContext.SaveChanges());

                return Ok();
            }

            return BadRequest(new {message = "HomeWork is empty"});
        }
    }
}