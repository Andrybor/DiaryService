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
    public class AuditoriumController : ControllerBase
    {
        DairyContext dbContext = new DairyContext();

        private readonly IMapper _mapper;

        public AuditoriumController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Auditorium> auditoriums = await dbContext
                .Auditorium.ToListAsync();

            var result = _mapper.Map<IEnumerable<AuditoriumVM>>(auditoriums);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Auditorium auditorium)
        {
            await Task.Run(() => dbContext.Auditorium.Add(auditorium));

            await Task.Run(() => dbContext.SaveChanges());

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var auditorium = await dbContext.Auditorium.FirstOrDefaultAsync(i => i.Id == id);

            if (auditorium != null)
            {
                await Task.Run(() => dbContext.Auditorium.Remove(auditorium));

                await Task.Run(() => dbContext.SaveChanges());

                return Ok();
            }

            return BadRequest(new { message = "Auditorium can't deleted" });
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Auditorium item)
        {
            var auditorium = await dbContext.Auditorium.FirstOrDefaultAsync(i => i.Id == item.Id);
            if (auditorium != null)
            {
                auditorium.Title = item.Title;
               
                await Task.Run(() => dbContext.Auditorium.Update(auditorium));

                await Task.Run(() => dbContext.SaveChanges());


                return Ok();
            }

            return BadRequest(new { message = "Auditorium can't edited" });
        }
    }
}