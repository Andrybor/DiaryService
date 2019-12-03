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
    public class AccountTypeController : ControllerBase
    {
        DairyContext dbContext = new DairyContext();

        private readonly IMapper _mapper;

        public AccountTypeController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<AccountType> accounts = await dbContext.AccountType.ToListAsync();

            var result = _mapper.Map<IEnumerable<AccountTypeVM>>(accounts);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromBody]string selectedAccountType)
        {
            if (!string.IsNullOrEmpty(selectedAccountType))
            {
                var accountType = await dbContext.AccountType.FirstOrDefaultAsync(i => i.Type == selectedAccountType);

                if (accountType != null)
                    return Ok(accountType.Id);
                else
                {
                    return BadRequest(new { message = "Account type not found" });
                }
            }
            else
                return BadRequest(new { message = "Selected account type is empty" });
        }
    }
}