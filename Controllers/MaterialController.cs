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
    public class MaterialController : ControllerBase
    {
        DairyContext dbContext = new DairyContext();

        private readonly IMapper _mapper;

        public MaterialController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var materials = await dbContext.Material.ToListAsync();

            return Ok(materials);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Material material)
        {
            await Task.Run(()=>dbContext.Material.Add(material));

            await Task.Run(()=>dbContext.SaveChanges());

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var material = await dbContext
                .Material
                .FirstOrDefaultAsync(i => i.Id == id);
            if (material != null)
            {
                await Task.Run(() => dbContext.Material.Remove(material));

                await Task.Run(() => dbContext.SaveChanges());

                return Ok();
            }

            return BadRequest(new {message = "Can't delete material"});
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Material item)
        {
            var material = await dbContext
                .Material
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == item.Id);

            if (material != null)
            {
                await Task.Run(() => dbContext.Material.Update(material));

                await Task.Run(() => dbContext.SaveChanges());

                return Ok();
            }
            return BadRequest(new { message = "Can't edit material" });
        }
    }
}