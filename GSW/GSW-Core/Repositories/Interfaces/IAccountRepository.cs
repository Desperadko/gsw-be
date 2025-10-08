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
        Task<Account?> GetById(int id);
        Task<int> Add(Account account);

        Task<bool> IdExists(int id);
        Task<bool> UsernameExists(string username);
        Task<bool> EmailExists(string email);
    }
}
