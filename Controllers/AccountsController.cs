using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleDataService.BAL;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpleDataService.DAL;
using SimpleDataService.Helpers;
using SimpleDataService.Services;

namespace SimpleDataService.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        DairyContext dbContext = new DairyContext();

        private readonly IMapper _mapper;
        private IAccountService _accountService;
        private readonly AppSettings _appSettings;

        public AccountsController(
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            IAccountService accountService
            )
        {
            _mapper = mapper;

            _accountService = accountService;

            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Account> accounts = await dbContext
                .Account
                .Include(user => user.User)
                .Include(type => type.AccountType).ToListAsync();

            if (!accounts.Any())
                return BadRequest(new {message = "Accounts Not Found"});

            var result = _mapper.Map<IEnumerable<AccountVM>>(accounts);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody]AccountVM account)
        {
            var _account = await _accountService.Authenticate(account.Login, account.Password);

            if (_account == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, _account.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);


            var result = _mapper.Map<AccountVM>(_account);

            return Ok(new
            {
                Account = result,
                Token = tokenString
            });
        }


        [HttpGet("{id}")]
        public ActionResult<Account> Get(int id)
        {
            // not realized yet
            return new NotFoundResult();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountVM accountVM)
        {
            // validation
            if (string.IsNullOrWhiteSpace(accountVM.Password))
                return BadRequest(new { message = "Password is required" });

            if (await Task.Run(()=>dbContext.Account.Any(x => x.Login == accountVM.Login)))
                return BadRequest(new { message = "Username \"" + accountVM.Login + "\" is already taken" });

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(accountVM.Password, out passwordHash, out passwordSalt);

            var account = _mapper.Map<Account>(accountVM);

            account.PasswordHash = passwordHash;
            account.PasswordSalt = passwordSalt;

            await Task.Run(() => dbContext.Account.Add(account));
            await Task.Run(() => dbContext.SaveChanges());


            return Ok(account.User.Id);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var account = await dbContext
                .Account
                .Include(i => i.AccountType)
                .FirstOrDefaultAsync(i => i.Id == id);

            var user = await dbContext.User.FirstOrDefaultAsync(x => x.AccountId == account.Id);

            if (account != null && user != null)
            {
                if (account.AccountType.Type == "Student")
                {
                    var student = await dbContext.Student.FirstOrDefaultAsync(i => i.UserId == user.Id);
                    if (student != null)
                        await Task.Run(()=>dbContext.Student.Remove(student));
                }

                if (account.AccountType.Type == "Teacher")
                {
                    var skills = await dbContext.TeacherSkill.Where(i => i.TeacherId == user.Id).ToListAsync();
                    if (skills != null && skills.Count() != 0)
                        foreach (var skill in skills)
                        {
                            await Task.Run(() => dbContext.TeacherSkill.Remove(skill));
                        }
                }

                await Task.Run(() => dbContext.User.Remove(user));
                await Task.Run(() => dbContext.Account.Remove(account));
                await Task.Run(() => dbContext.SaveChanges());

                if (await dbContext.Account.FirstOrDefaultAsync(x=>x.Id == id) !=null)
                    return BadRequest(new { message = "Account not deleted" });
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] AccountVM accountVM)
        {
            if (accountVM != null)
            {
                var user = _mapper.Map<User>(accountVM.User);
                accountVM.User = null;
                accountVM.AccountType = null;

                var acc = await dbContext.Account.FirstOrDefaultAsync(i => i.Id == accountVM.Id);
                if (accountVM.Password != null)
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash(accountVM.Password, out passwordHash, out passwordSalt);
                    acc.PasswordHash = passwordHash;
                    acc.PasswordSalt = passwordSalt;
                }

                acc.Login = accountVM.Login;

                
                await Task.Run(() => dbContext.User.Update(user));
                await Task.Run(() => dbContext.SaveChanges());

                return Ok();
            }

            return BadRequest(new { message = "Account is empty" }); 
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}