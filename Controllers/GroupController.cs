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
    public class GroupController : ControllerBase
    {
        DairyContext dbContext = new DairyContext();

        private readonly IMapper _mapper;

        public GroupController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Group> groups =await dbContext
                .Group
                .Include(i=>i.Course)
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<GroupVM>>(groups);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var group = await dbContext
                .Group
                .Include(i=>i.Student)
                .ThenInclude(i=>i.User)
                .Include(i => i.Course)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (group != null)
            {   
                return Ok(_mapper.Map<GroupVM>(group));
            }

            return BadRequest(new { message = "Group not found" }); 
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Group group)
        {
            await Task.Run(()=>dbContext.Group.Add(group));

            await Task.Run(() => dbContext.SaveChanges());

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var group = await dbContext
                .Group
                .FirstOrDefaultAsync(i => i.Id == id);

            if (group != null)
            {
                await Task.Run(() => dbContext.Group.Remove(group));

                await Task.Run(() => dbContext.SaveChanges());

                return Ok();
            }

            return BadRequest(new { message = "Group can't deleted" });
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Group item)
        {
            var group = await dbContext
                .Group
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == item.Id);

            if (group != null)
            {
                group.Title = item.Title;

                await Task.Run(() => dbContext.Group.Update(group));

                await Task.Run(() => dbContext.SaveChanges());

                return Ok();
            }

            return BadRequest(new { message = "Group can't edited" });
        }
    }
}