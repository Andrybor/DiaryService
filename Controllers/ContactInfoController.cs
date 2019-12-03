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
    public class ContactInfoController : ControllerBase
    {
        DairyContext dbContext = new DairyContext();

        private readonly IMapper _mapper;

        public ContactInfoController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<ContactInfo> contacts =await dbContext
                .ContactInfo.ToListAsync();

            return Ok(contacts);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContactInfo contact)
        {
            await Task.Run(()=>dbContext.ContactInfo.Add(contact));

            await Task.Run(() => dbContext.SaveChanges());

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var contact = await dbContext
                .ContactInfo
                .FirstOrDefaultAsync(i => i.Id == id);

            if (contact != null)
            {
                await Task.Run(() => dbContext.ContactInfo.Remove(contact));

                await Task.Run(() => dbContext.SaveChanges());

                return Ok();
            }

            return BadRequest(new { message = "Contact info can't deleted" });
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] ContactInfo item)
        {           
            await Task.Run(() => dbContext.ContactInfo.Update(item));

            await Task.Run(() => dbContext.SaveChanges());

            return Ok();

        }
    }
   
}