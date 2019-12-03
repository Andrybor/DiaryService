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
    public class NewsController : ControllerBase
    {
        DairyContext dbContext = new DairyContext();

        private readonly IMapper _mapper;

        public NewsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<News> news = await dbContext
                .News
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<NewsVM>>(news);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] News news)
        {
            await Task.Run(()=>dbContext.News.Add(news));

            await Task.Run(() => dbContext.SaveChanges());

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var news = dbContext.News.FirstOrDefault(i => i.Id == id);
            if (news != null)
            {
                await Task.Run(() =>
                    dbContext.News.Remove(news));

                await Task.Run(() => dbContext.SaveChanges());

                return Ok();
            }

            return BadRequest(new {message = "Can't delete news"});
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] News item)
        {
            var news =await dbContext
                .News
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == item.Id);

            if (news != null)
            {
                await Task.Run(() => dbContext.News.Update(news));

                await Task.Run(() => dbContext.SaveChanges());

                return Ok();
            }

            return BadRequest(new { message = "Can't edit news" });
        }
    }
}