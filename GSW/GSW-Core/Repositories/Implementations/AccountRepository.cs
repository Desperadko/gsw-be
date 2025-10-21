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

        public async Task<Account?> GetById(int id)
        {
            return await dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Account?> GetByUsername(string username)
        {
            return await dbContext.Accounts.FirstOrDefaultAsync(a => a.Username == username);
        }

        public async Task<int> Add(Account account)
        {
            await dbContext.Accounts.AddAsync(account);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<bool> IdExists(int id)
        {
            return await dbContext.Accounts.AnyAsync(a => a.Id == id);
        }

        public async Task<bool> UsernameExists(string username)
        {
            return await dbContext.Accounts.AnyAsync(a => a.Username == username);
        }

        public async Task<bool> EmailExists(string email)
        {
            return await dbContext.Accounts.AnyAsync(a => a.Email == email);
        }

        public async Task<int> SaveChanges()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}
