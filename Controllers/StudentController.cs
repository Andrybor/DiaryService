using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public class StudentController : ControllerBase
    {
        DairyContext dbContext = new DairyContext();

        private readonly IMapper _mapper;

        public StudentController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Student> students = await dbContext
                .Student
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<StudentVM>>(students);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var student = await dbContext
                .Student
                .Include(i=>i.Group)
                .FirstOrDefaultAsync(i => i.UserId == id);

            if (student != null)
                return Ok(_mapper.Map<StudentVM>(student));

            return BadRequest(new{message="Student isn't found"});
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Student student)
        {
            await Task.Run(()=>dbContext.Student.Add(student));

            await Task.Run(() => dbContext.SaveChanges());

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var student = await dbContext.Student.FirstOrDefaultAsync(i => i.Id == id);

            if (student != null)
            {
                await Task.Run(()=> dbContext.Student.Remove(student));

                await Task.Run(() => dbContext.SaveChanges());

                return Ok();
            }

            return BadRequest(new {message = "Student can't deleted"});
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Student item)
        {
            await Task.Run(() => dbContext.Student.Update(item));

            await Task.Run(() => dbContext.SaveChanges());

            return Ok();
        }
    }
}
