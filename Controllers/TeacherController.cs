using System;
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
    public class TeacherController : ControllerBase
    {
        DairyContext dbContext = new DairyContext();

        private readonly IMapper _mapper;

        public TeacherController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<TeacherSkill> teachers = await dbContext
                .TeacherSkill
                .Include(x=>x.Subject)
                .Include(x=>x.Teacher)
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<TeacherVM>>(teachers);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] List<TeacherSkill> skills)
        {
            foreach (var skill in skills)
            {
                var isExistSkill =await dbContext.TeacherSkill.FirstOrDefaultAsync(i =>
                    i.TeacherId == skill.TeacherId && i.SubjectId == skill.SubjectId);
                if (isExistSkill == null)
                {
                    await Task.Run(()=>dbContext.TeacherSkill.Add(skill));

                    await Task.Run(() => dbContext.SaveChanges());
                } 
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var teacher =await dbContext.TeacherSkill.FirstOrDefaultAsync(i => i.TeacherId == id);
            if (teacher != null)
            {
                await Task.Run(() => dbContext.TeacherSkill.Remove(teacher));

                await Task.Run(()=>dbContext.SaveChanges());

                return Ok();
            }

            return BadRequest(new {message = "Can't deleted teacher skill"});
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] TeacherSkill item)
        {
            var teacher = await dbContext.TeacherSkill.AsNoTracking().FirstOrDefaultAsync(i => i.TeacherId == item.TeacherId);
            if (teacher != null)
            {
                await Task.Run(()=>dbContext.TeacherSkill.Update(item));

                await Task.Run(()=>dbContext.SaveChanges());

                return Ok();
            }

            return BadRequest(new {message = "Can't edit teacher skill"});
        }
    }
}