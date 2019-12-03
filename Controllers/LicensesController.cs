using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleDataService.DAL;

namespace SimpleDataService.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LicensesController : ControllerBase
    {
        DairyContext dbContext = new DairyContext();

        private readonly IMapper _mapper;

        public LicensesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Licenses> licenses = await dbContext
                .Licenses
                .ToListAsync();

            return Ok(licenses);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] int count)
        {
            for (int i = 0; i < count; i++)
            {
                Licenses license = new Licenses
                {
                    Id = Guid.NewGuid(),
                    IsAssigned = false
                };

                await Task.Run(() => dbContext.Licenses.Add(license));
            }
           
            await Task.Run(() => dbContext.SaveChanges());

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyLicense([FromBody] Guid id)
        {
            var licenses = await dbContext.Licenses.FirstOrDefaultAsync(i => i.Id == id);
            if (licenses != null)
            {
                await Task.Run(() => dbContext.Licenses.Remove(licenses));

                return Ok(true);
            }

            return BadRequest(new { message = "License not avaible" });
        }

        [HttpPost]
        public async Task<IActionResult> AssignLicense([FromBody] Guid id)
        {
            var licenses = await dbContext.Licenses.FirstOrDefaultAsync(i => i.Id == id);
            if (licenses != null)
            {
                licenses.IsAssigned = true;

                await Task.Run(() => dbContext.Licenses.Update(licenses));

                await Task.Run(() => dbContext.SaveChanges());

                return Ok();
            }

            return BadRequest(new { message = "License not avaible" });
        }
    }
}
