using GSW_Core.DTOs;
using GSW_Core.Repositories.Interfaces;
using GSW_Data;
using GSW_Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Repositories.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private readonly GSWDbContext dbContext;

        public AccountRepository(GSWDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Account?> GetByIdAsync(int id)
        {
            return await dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Account?> GetByUsernameAsync(string username)
        {
            return await dbContext.Accounts.FirstOrDefaultAsync(a => a.Username == username);
        }

        public async Task<int> AddAsync(Account account)
        {
            await dbContext.Accounts.AddAsync(account);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<bool> IdExistsAsync(int id)
        {
            return await dbContext.Accounts.AnyAsync(a => a.Id == id);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await dbContext.Accounts.AnyAsync(a => a.Username == username);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await dbContext.Accounts.AnyAsync(a => a.Email == email);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}
