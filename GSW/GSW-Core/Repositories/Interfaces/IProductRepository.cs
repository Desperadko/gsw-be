using GSW_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetAsync(int id);
        Task<int> AddAsync(Product product);
    }
}
