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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        DairyContext dbContext = new DairyContext();

        private readonly IMapper _mapper;

        public SpecializationController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Specialization> specializations = await dbContext
                .Specialization
                .Include(spec => spec.Course)
                .Include(spec => spec.Group)
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<SpecializationVM>>(specializations);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Specialization specialization)
        {
           await Task.Run(()=>dbContext.Specialization.Add(specialization));

           await Task.Run(() => dbContext.SaveChanges());

           return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var specialization = await dbContext.Specialization.FirstOrDefaultAsync(i=>i.Id == id);
            if (specialization != null)
            {
                await Task.Run(() => dbContext.Specialization.Remove(specialization));

                await Task.Run(() => dbContext.SaveChanges());

                return Ok();
            }

            return BadRequest(new {message = "Can't delete specialization"});
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Specialization item)
        {
            var specialization = await dbContext.Specialization.FirstOrDefaultAsync(i => i.Id == item.Id);
            if (specialization != null)
            {
                specialization.Title = item.Title;
                await Task.Run(()=>dbContext.Specialization.Update(specialization));

                await Task.Run(()=>dbContext.SaveChanges());

                return Ok();
            }

            return BadRequest(new {message = "Can't edit specialization"});
        }
    }
}