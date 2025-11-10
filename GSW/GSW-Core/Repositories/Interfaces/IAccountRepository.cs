using GSW_Core.DTOs;
using GSW_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account?> GetByIdAsync(int id);
        Task<Account?> GetByUsernameAsync(string username);
        Task<int> AddAsync(Account account);

        Task<bool> IdExistsAsync(int id);
        Task<bool> UsernameExistsAsync(string username);
        Task<bool> EmailExistsAsync(string email);

        Task<int> SaveChangesAsync();
    }
}
