using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleDataService.DAL;
using SimpleDataService.Helpers;

namespace SimpleDataService.Services
{
    public interface IAccountService
    {
        Task<Account> Authenticate(string login, string password);
        IEnumerable<Account> GetAll();
        Account GetById(int id);
    }

    public class AccountService : IAccountService
    {
        private readonly DairyContext _dbContext = new DairyContext();

        public async Task<Account> Authenticate(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return null;

            var account = await _dbContext
                .Account
                .Include(i => i.User)
                .Include(i => i.AccountType)
                .SingleOrDefaultAsync(i => i.Login == login);

            // return null if user not found
            if (account == null)
                return null;

            if (!VerifyPassword(password, account.PasswordHash, account.PasswordSalt))
                return null;

            return account;
        }

        public IEnumerable<Account> GetAll()
        {
            // return users without passwords
            return  _dbContext.Account;
        }

        public Account GetById(int id)
        {
            return _dbContext.Account.Find(id);
        }

        public static bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            return VerifyPasswordHash(password,storedHash,storedSalt);
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
